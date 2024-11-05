using System.ComponentModel.DataAnnotations;

namespace TextAnalysisMicroservice.Models.Requests
{
    public class CountWordsRequest
    {
        [Required(ErrorMessage = "InputText is required")]
        public string InputText { get; set; }

        [Required(ErrorMessage = "Words list is required")]
        [MinLength(1, ErrorMessage = "The words list must contain at least one word")]
        public List<string> Words { get; set; }
    }
}
