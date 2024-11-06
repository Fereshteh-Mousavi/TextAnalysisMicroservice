using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAnalysisMicroservice.Helpers;
using TextAnalysisMicroservice.Services.Interfaces;

namespace Tests
{
    [TestClass]
    public class TextAnalysisServiceTests
    {
        private ITextAnalysisService _textAnalysisService;

        [TestInitialize]
        public void Setup()
        {
            _textAnalysisService = new TextAnalysisService();
        }

        [TestMethod]
        public void CountWords_ShouldReturnCorrectCounts_ForCaseInsensitiveMatches()
        {
            var input = "Apple orange apple banana";
            var words = new List<string> { "apple", "banana" };
            var result = _textAnalysisService.CountWords(input, words);

            Assert.AreEqual(2, result["apple"]);
            Assert.AreEqual(1, result["banana"]);
        }

        [TestMethod]
        public void ContainsWords_ShouldDetectWords_CaseInsensitively()
        {
            var input = "Apple orange";
            var words = new List<string> { "apple", "banana" };
            var result = _textAnalysisService.ContainsWords(input, words);

            Assert.IsTrue(result["apple"]);
            Assert.IsFalse(result["banana"]);
        }

        [TestMethod]
        public void IsBase64_ShouldReturnTrue_ForValidBase64()
        {
            Assert.IsTrue(TextValidator.IsBase64("U29tZSBiYXNlNjQgc3RyaW5n"));
            Assert.IsFalse(TextValidator.IsBase64("invalid-string"));
        }

        [TestMethod]
        public void IsValidEmail_ShouldValidateProperly()
        {
            Assert.IsTrue(TextValidator.IsValidEmail("test@example.com"));
            Assert.IsFalse(TextValidator.IsValidEmail("invalid-email"));
        }

        [TestMethod]
        public void ConvertToDecimal_ValidInputs_ReturnsExpectedResults()
        {
            Assert.AreEqual(1600500.3025m, TextValidator.ConvertToDecimal("1,600,500.3025"));

            Assert.AreEqual(1600500.3025m, TextValidator.ConvertToDecimal("1.600.500,3025"));

            Assert.AreEqual(12345.67m, TextValidator.ConvertToDecimal("12345.67"));

            Assert.IsNull(TextValidator.ConvertToDecimal("1.2.3"));

            Assert.IsNull(TextValidator.ConvertToDecimal("1,2a3"));

            Assert.IsNull(TextValidator.ConvertToDecimal("1,000,000,500"));

            Assert.IsNull(TextValidator.ConvertToDecimal(""));

            Assert.IsNull(TextValidator.ConvertToDecimal(null));
        }


        [TestMethod]
        public void ConvertToDecimal_InvalidInputs_ReturnsNull()
        {
            Assert.IsNull(TextValidator.ConvertToDecimal("abc"));
            Assert.IsNull(TextValidator.ConvertToDecimal("1.2.3"));
            Assert.IsNull(TextValidator.ConvertToDecimal("1,2a3"));
            Assert.IsNull(TextValidator.ConvertToDecimal("1,000,500,3025")); 
        }



    }

}