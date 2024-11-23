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

        public UsuarioPredictionController(UsuarioPredictionService predictionService)
        {
            _predictionService = predictionService;
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
            if (input == null)
                return BadRequest("Os dados de entrada são inválidos.");

            var prediction = _predictionService.Predict(input);
            return Ok(prediction);
        }
    }
}