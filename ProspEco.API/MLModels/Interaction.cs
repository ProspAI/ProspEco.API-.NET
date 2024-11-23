// ProspEco.API/MLModels/Interaction.cs
using Microsoft.ML.Data;

namespace ProspEco.API.MLModels
{
    public class Interaction
    {
        [LoadColumn(0)]
        public string UserId { get; set; }

        [LoadColumn(1)]
        public string ItemId { get; set; }

        [LoadColumn(2)]
        public float Rating { get; set; }
    }
}
