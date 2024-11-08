﻿using System.Text.RegularExpressions;
using TextAnalysisMicroservice.Helpers;
using TextAnalysisMicroservice.Services.Interfaces;

public class TextAnalysisService : ITextAnalysisService
{
    public Dictionary<string, int> CountWords(string input, List<string> words)
    {
        var result = new Dictionary<string, int>();
        foreach (var word in words)
        {
            result[word] = Regex.Matches(input, $@"\b{Regex.Escape(word)}\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Count;
        }
        return result;
    }


    public Dictionary<string, bool> ContainsWords(string input, List<string> words)
    {
        var result = new Dictionary<string, bool>();
        foreach (var word in words)
        {
            string pattern = $@"\b{Regex.Escape(word)}\b";
            result[word] = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }
        return result;
    }

    public bool IsBase64(string input)
    {
        return TextValidator.IsBase64(input);
    }

    public bool IsValidEmail(string input)
    {
        return TextValidator.IsValidEmail(input);
    }

    public decimal? ConvertToDecimal(string input)
    {
        return TextValidator.ConvertToDecimal(input);
    }
}
