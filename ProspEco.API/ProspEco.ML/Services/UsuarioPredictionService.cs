using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using ProspEco.ML.MLModels;

namespace ProspEco.ML.Services
{
    public class UsuarioPredictionService
    {
        private const string DataPath = "ProspEco.ML/data/dados_modelo.csv";
        private const string ModelPath = "ProspEco.ML/modelo_usuario.zip";

        private readonly MLContext _mlContext;
        private ITransformer _trainedModel;

        public UsuarioPredictionService()
        {
            _mlContext = new MLContext();
        }

        public void TrainModel()
        {
            // Passo 1: Carregar os dados
            var dataView = _mlContext.Data.LoadFromTextFile<UsuarioDataCsv>(
                path: DataPath,
                hasHeader: true,
                separatorChar: ','
            );

            // Passo 2: Pré-processar os dados
            var dataProcessPipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(UsuarioDataCsv.Email))
                                       .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(UsuarioDataCsv.Nome)));

            // Passo 3: Escolher e configurar o algoritmo de treinamento
            var trainer = _mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features")
                         .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            // Passo 4: Treinar o modelo
            _trainedModel = trainingPipeline.Fit(dataView);

            // Passo 5: Salvar o modelo treinado
            _mlContext.Model.Save(_trainedModel, dataView.Schema, ModelPath);
        }

        public UsuarioPrediction Predict(UsuarioData input)
        {
            // Verificar se o modelo foi carregado
            if (_trainedModel == null)
            {
                if (!File.Exists(ModelPath))
                    throw new InvalidOperationException("O modelo precisa ser treinado antes de fazer previsões.");

                // Carregar modelo salvo
                using var fileStream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                _trainedModel = _mlContext.Model.Load(fileStream, out _);
            }

            // Criar um engine de previsão
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<UsuarioDataCsv, UsuarioPrediction>(_trainedModel);

            // Converter entrada para o formato esperado
            var inputCsv = new UsuarioDataCsv
            {
                ID = input.ID.ToString(),
                Nome = input.Nome,
                Email = input.Email
            };

            // Fazer a previsão
            return predictionEngine.Predict(inputCsv);
        }

        // Classe auxiliar para mapear os dados do CSV
        private class UsuarioDataCsv
        {
            [LoadColumn(0)]
            public string ID { get; set; }

            [LoadColumn(1)]
            public string Nome { get; set; }

            [LoadColumn(2)]
            public string Email { get; set; }
        }
    }
}
