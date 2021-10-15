using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

namespace classifier_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictController : Controller
    {
        // https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/serve-model-web-api-ml-net
        // https://dev.to/willvelida/building-a-price-prediction-api-using-ml-net-and-asp-net-core-web-api-part-1-447j

        private readonly PredictionEnginePool<SentimentData, SentimentPrediction> _predictionEnginePool;

        public PredictController(PredictionEnginePool<SentimentData, SentimentPrediction> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        // https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(RequestObject request)
        {
            if (string.IsNullOrEmpty(request.Text))
            {
                return BadRequest(new ErrorObject
                {
                    Message = "Bad Request"
                });
            }

            SentimentData example = new SentimentData()
            {
                SentimentText = request.Text,
            };

            SentimentPrediction prediction = _predictionEnginePool.Predict(modelName: "SentimentAnalysisModel", example: example);

            return Ok(new ResponseObject
            {
                Intent = prediction.Sentiment,
                Confidence = prediction.Scores.Max(),
            });
        }
    }
}
