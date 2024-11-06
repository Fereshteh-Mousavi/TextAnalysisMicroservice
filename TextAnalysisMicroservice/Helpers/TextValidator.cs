using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TextAnalysisMicroservice.Helpers
{
    public static class TextValidator
    {
        public static bool IsBase64(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            input = input.Trim().Replace("\n", "").Replace("\r", "");

            if (input.Length % 4 != 0)
                return false;

            return Regex.IsMatch(input, @"^[A-Za-z0-9+/]*={0,2}$");
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

            string sanitizedInput = input.Trim();

            int commaCount = sanitizedInput.Count(c => c == ',');
            int periodCount = sanitizedInput.Count(c => c == '.');

            if (commaCount > 1 && periodCount > 1)
            {
                return null;
            }
            if (commaCount == 1 && periodCount == 1 && sanitizedInput.IndexOf(',') > sanitizedInput.IndexOf('.'))
            {
                return null;
            }
            if (commaCount > 0 && periodCount > 0 && Math.Abs(sanitizedInput.IndexOf(',') - sanitizedInput.IndexOf('.')) == 1)
            {
                return null;
            }

            if (commaCount == 1 && periodCount > 1)
            {
                sanitizedInput = sanitizedInput.Replace(".", "").Replace(",", ".");
            }
            else if (periodCount == 1 && commaCount > 1)
            {
                sanitizedInput = sanitizedInput.Replace(",", "");
            }
            else if (commaCount == 1 && periodCount == 0)
            {
                sanitizedInput = sanitizedInput.Replace(",", ".");
            }
            else if (periodCount == 1 && commaCount == 0)
            {
            }
            else if (commaCount > 1 || periodCount > 1)
            {
                return null;
            }

            if (decimal.TryParse(sanitizedInput, System.Globalization.NumberStyles.AllowDecimalPoint,
                                 System.Globalization.CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            return null;
        }

    }
}
