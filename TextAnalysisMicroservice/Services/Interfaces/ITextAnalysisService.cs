namespace TextAnalysisMicroservice.Services.Interfaces
{
    public interface ITextAnalysisService
    {
        Dictionary<string, int> CountWords(string input, List<string> words);
        Dictionary<string, bool> ContainsWords(string input, List<string> words);
        bool IsBase64(string input);
        bool IsValidEmail(string input);
        decimal? ConvertToDecimal(string input);
    }
}
