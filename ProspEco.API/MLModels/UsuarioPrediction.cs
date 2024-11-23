namespace ProspEco.API.MLModels
{
    public class UsuarioPrediction
    {
        public string PredictedLabel { get; set; }
        public float[] Score { get; set; }
    }
}
