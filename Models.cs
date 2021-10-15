using Microsoft.ML.Data;
using Newtonsoft.Json;

namespace classifier_service
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public string Sentiment { get; set; }

        [LoadColumn(1)]
        public string SentimentText { get; set; }
    }

    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Sentiment;

        [ColumnName("Score")]
        public float[] Scores { get; set; }
    }

    public class ErrorObject
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class RequestObject
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class ResponseObject
    {
        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("confidence")]
        public float Confidence { get; set; }
    }
}
