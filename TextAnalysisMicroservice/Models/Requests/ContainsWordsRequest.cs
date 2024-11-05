namespace TextAnalysisMicroservice.Models.Requests
{
    public class ContainsWordsRequest
    {
        public string InputText { get; set; }
        public List<string> Words { get; set; }
    }
}
