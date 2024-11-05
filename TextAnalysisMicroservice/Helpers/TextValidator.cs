using System;
using System.Text.RegularExpressions;

namespace TextAnalysisMicroservice.Helpers
{
    public static class TextValidator
    {
        public static bool IsBase64(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            input = input.Trim();
            return (input.Length % 4 == 0) &&
                   Regex.IsMatch(input, @"^(?:[A-Za-z0-9+/=]*|\s*)$", RegexOptions.None);
        }

        public static bool IsValidEmail(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            try
            {
                var email = new System.Net.Mail.MailAddress(input);
                return email.Address == input;
            }
            catch
            {
                return false;
            }
        }

        public static decimal? ConvertToDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            
            string sanitizedInput = input.Replace(" ", "")
                                         .Replace("_", "")
                                         .Replace("m", "")
                                         .Replace("'", "");

            
            int lastCommaIndex = sanitizedInput.LastIndexOf(',');
            int lastDotIndex = sanitizedInput.LastIndexOf('.');

            if (lastCommaIndex > lastDotIndex && lastCommaIndex != -1)
            {
                sanitizedInput = sanitizedInput.Replace(".", ""); 
                sanitizedInput = ReplaceLastOccurrence(sanitizedInput, ",", "."); 
            }
            else if (lastDotIndex > lastCommaIndex && lastDotIndex != -1)
            {
                
                sanitizedInput = sanitizedInput.Replace(",", "");  
            }
            else
            {
               
                sanitizedInput = sanitizedInput.Replace(",", "").Replace(".", "");
            }

            
            if (decimal.TryParse(sanitizedInput, out decimal result))
            {
                return result;
            }

            return 0;
        }

        
        private static string ReplaceLastOccurrence(string source, string find, string replace)
        {
            int place = source.LastIndexOf(find);
            if (place == -1)
                return source;

            return source.Remove(place, find.Length).Insert(place, replace);
        }





    }
}
