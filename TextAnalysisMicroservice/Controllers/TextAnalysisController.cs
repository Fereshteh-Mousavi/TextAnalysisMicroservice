using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TextAnalysisMicroservice.Models.Requests;
using TextAnalysisMicroservice.Models.Responses;
using TextAnalysisMicroservice.Services.Interfaces;

namespace TextAnalysisMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextAnalysisController : ControllerBase
    {
        private readonly ITextAnalysisService _textAnalysisService;
        private readonly ILogger<TextAnalysisController> _logger;

        public TextAnalysisController(ITextAnalysisService textAnalysisService, ILogger<TextAnalysisController> logger)
        {
            _textAnalysisService = textAnalysisService;
            _logger = logger;
        }

        [HttpPost("count-words")]
        public IActionResult CountWords([FromBody] CountWordsRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<Dictionary<string, int>>
                {
                    Success = false,
                    Message = "Request cannot be null."
                });
            }

            if (string.IsNullOrWhiteSpace(request.InputText) || request.Words == null || !request.Words.Any())
            {
                return BadRequest(new ApiResponse<Dictionary<string, int>>
                {
                    Success = false,
                    Message = "Input text and words cannot be null or empty."
                });
            }

            try
            {
                var result = _textAnalysisService.CountWords(request.InputText, request.Words);
                return Ok(new ApiResponse<Dictionary<string, int>>
                {
                    Data = result,
                    Success = true,
                    Message = "Word counts retrieved successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing CountWords request.");
                return StatusCode(500, new ApiResponse<Dictionary<string, int>>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    ErrorDetails = ex.Message
                });
            }
        }

        [HttpPost("contains-words")]
        public IActionResult ContainsWords([FromBody] ContainsWordsRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<Dictionary<string, bool>>
                {
                    Success = false,
                    Message = "Request cannot be null."
                });
            }

            if (string.IsNullOrWhiteSpace(request.InputText) || request.Words == null || !request.Words.Any())
            {
                return BadRequest(new ApiResponse<Dictionary<string, bool>>
                {
                    Success = false,
                    Message = "Input text and words cannot be null or empty."
                });
            }

            try
            {
                var result = _textAnalysisService.ContainsWords(request.InputText, request.Words);
                return Ok(new ApiResponse<Dictionary<string, bool>>
                {
                    Data = result,
                    Success = true,
                    Message = "Words presence check completed successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing ContainsWords request.");
                return StatusCode(500, new ApiResponse<Dictionary<string, bool>>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    ErrorDetails = ex.Message
                });
            }
        }

        [HttpPost("convert-to-decimal")]
        public IActionResult ConvertToDecimal([FromBody] ConvertToDecimalRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Input))
            {
                return BadRequest(new ApiResponse<string>
                {
                    Data = null,
                    Message = "Input cannot be null or empty.",
                    Success = false,
                    ErrorDetails = "Invalid input provided."
                });
            }

            decimal? result = _textAnalysisService.ConvertToDecimal(request.Input);

            if (result.HasValue)
            {
                return Ok(new ApiResponse<object>
                {
                    Data = new
                    {
                        Input = request.Input,
                        Result = result.Value
                    },
                    Message = "Conversion successful.",
                    Success = true,
                    ErrorDetails = null
                });
            }
            else
            {
                return Ok(new ApiResponse<object>
                {
                    Data = new
                    {
                        Input = request.Input,
                        Result = 0
                    },
                    Message = "The input string is not a valid decimal format.",
                    Success = true,
                    ErrorDetails = null
                });
            }
        }


    }
}
