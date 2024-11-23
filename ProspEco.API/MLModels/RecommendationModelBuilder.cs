// ProspEco.API/MLModels/RecommendationModelBuilder.cs
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.IO;

namespace ProspEco.API.MLModels
{
    public class RecommendationModelBuilder
    {
        private readonly string _dataPath;
        private readonly string _modelPath;
        private readonly MLContext _mlContext;

        public RecommendationModelBuilder(string dataPath, string modelPath)
        {
            _dataPath = dataPath;
            _modelPath = modelPath;
            _mlContext = new MLContext();
        }

        public void TrainModel()
        {
            // Carregar os dados
            IDataView dataView = _mlContext.Data.LoadFromTextFile<Interaction>(
                path: Path.Combine(_dataPath, "interacoes.csv"),
                hasHeader: true,
                separatorChar: ',');

            // Preparar o pipeline de treinamento
            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(Interaction.UserId),
                MatrixRowIndexColumnName = nameof(Interaction.ItemId),
                LabelColumnName = nameof(Interaction.Rating),
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var estimator = _mlContext.Recommendation().Trainers.MatrixFactorization(options);

            // Treinar o modelo
            var model = estimator.Fit(dataView);

            // Salvar o modelo
            _mlContext.Model.Save(model, dataView.Schema, _modelPath);

            Console.WriteLine("Modelo treinado e salvo em: " + _modelPath);
        }

        public ITransformer LoadModel()
        {
            DataViewSchema inputSchema;
            var model = _mlContext.Model.Load(_modelPath, out inputSchema);
            return model;
        }
    }
}
