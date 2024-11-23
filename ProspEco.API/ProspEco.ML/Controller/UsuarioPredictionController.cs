using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using ProspEco.ML.MLModels;
using ProspEco.ML.Services;

namespace ProspEco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioPredictionController : ControllerBase
    {
        private readonly UsuarioPredictionService _predictionService;

        public UsuarioPredictionController()
        {
            _predictionService = new UsuarioPredictionService();
        }

        [HttpPost("train")]
        public IActionResult TrainModel()
        {
            _predictionService.TrainModel();
            return Ok("Modelo treinado com sucesso.");
        }

        [HttpPost("predict")]
        public ActionResult<UsuarioPrediction> Predict([FromBody] UsuarioData input)
        {
            var prediction = _predictionService.Predict(input);
            return Ok(prediction);
        }
    }
}
