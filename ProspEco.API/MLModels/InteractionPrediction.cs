// ProspEco.API/MLModels/InteractionPrediction.cs
using Microsoft.ML.Data;

namespace ProspEco.API.MLModels
{
    public class InteractionPrediction
    {
        [ColumnName("Score")]
        public float Rating { get; set; }
    }
}