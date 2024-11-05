using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TextAnalysisMicroservice.Services;
using TextAnalysisMicroservice.Services.Interfaces;

namespace TextAnalysisMicroserviceTests
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

        #region CountWords Tests

        [TestMethod]
        public void CountWords_ShouldReturnCorrectCounts_WhenWordsExistInInput()
        {
            // Arrange
            string input = "apple orange apple banana";
            List<string> words = new List<string> { "apple", "banana", "grape" };

            // Act
            var result = _textAnalysisService.CountWords(input, words);

            // Assert
            Assert.AreEqual(2, result["apple"]);
            Assert.AreEqual(1, result["banana"]);
            Assert.AreEqual(0, result["grape"]);
        }

        [TestMethod]
        public void CountWords_ShouldReturnZeroCounts_WhenWordsDoNotExistInInput()
        {
            // Arrange
            string input = "apple orange apple banana";
            List<string> words = new List<string> { "pear", "grape" };

            // Act
            var result = _textAnalysisService.CountWords(input, words);

            // Assert
            Assert.AreEqual(0, result["pear"]);
            Assert.AreEqual(0, result["grape"]);
        }

        #endregion

        #region ContainsWords Tests

        [TestMethod]
        public void ContainsWords_ShouldReturnTrue_WhenWordsArePresentInInput()
        {
            // Arrange
            string input = "apple orange apple banana";
            List<string> words = new List<string> { "apple", "banana" };

            // Act
            var result = _textAnalysisService.ContainsWords(input, words);

            // Assert
            Assert.IsTrue(result["apple"]);
            Assert.IsTrue(result["banana"]);
        }

        [TestMethod]
        public void ContainsWords_ShouldReturnFalse_WhenWordsAreNotPresentInInput()
        {
            // Arrange
            string input = "apple orange apple banana";
            List<string> words = new List<string> { "grape", "pear" };

            // Act
            var result = _textAnalysisService.ContainsWords(input, words);

            // Assert
            Assert.IsFalse(result["grape"]);
            Assert.IsFalse(result["pear"]);
        }

        #endregion

        #region IsBase64 Tests

        [TestMethod]
        public void IsBase64_ShouldReturnTrue_ForValidBase64Strings()
        {
            // Arrange
            string validBase64 = "U29tZSBiYXNlNjQgc3RyaW5n"; // "Some base64 string"

            // Act
            bool result = _textAnalysisService.IsBase64(validBase64);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsBase64_ShouldReturnFalse_ForInvalidBase64Strings()
        {
            // Arrange
            string invalidBase64 = "This is not base64";

            // Act
            bool result = _textAnalysisService.IsBase64(invalidBase64);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region IsValidEmail Tests

        [TestMethod]
        public void IsValidEmail_ShouldReturnTrue_ForValidEmailAddresses()
        {
            // Arrange
            string validEmail = "test@example.com";

            // Act
            bool result = _textAnalysisService.IsValidEmail(validEmail);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidEmail_ShouldReturnFalse_ForInvalidEmailAddresses()
        {
            // Arrange
            string invalidEmail = "invalid-email@";

            // Act
            bool result = _textAnalysisService.IsValidEmail(invalidEmail);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region ConvertToDecimal Tests

        [TestMethod]
        public void ConvertToDecimal_ShouldReturnCorrectDecimal_ForValidFormattedString()
        {
            // Arrange
            string validInput = "1,600,500.3025"; // US format

            // Act
            decimal? result = _textAnalysisService.ConvertToDecimal(validInput);

            // Assert
            Assert.AreEqual(1600500.3025m, result);
        }

        [TestMethod]
        public void ConvertToDecimal_ShouldReturnZero_ForInvalidDecimalString()
        {
            // Arrange
            string invalidInput = "f1,600,500.3025";

            // Act
            decimal? result = _textAnalysisService.ConvertToDecimal(invalidInput);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ConvertToDecimal_ShouldHandleVariousFormats()
        {
            // Arrange
            string inputWithSpaces = "1 600 500.3025"; // Should parse to 1600500.3025
            string inputWithUnderscores = "1_600_500.3025"; // Should parse to 1600500.3025
            string inputWithMCharacter = "1,6.00,500.3025m"; // Should parse to 1600500.3025
            string inputWithMultipleFormats = "1,600.500,3025"; // Mixed formats, should return 1600500.3025

            // Act & Assert
            Assert.AreEqual(1600500.3025m, _textAnalysisService.ConvertToDecimal(inputWithSpaces));
            Assert.AreEqual(1600500.3025m, _textAnalysisService.ConvertToDecimal(inputWithUnderscores));
            Assert.AreEqual(1600500.3025m, _textAnalysisService.ConvertToDecimal(inputWithMCharacter));
            Assert.AreEqual(1600500.3025m, _textAnalysisService.ConvertToDecimal(inputWithMultipleFormats));
        }

        [TestMethod]
        public void ConvertToDecimal_ShouldReturnNull_WhenInputIsNullOrEmpty()
        {
            // Arrange
            string nullInput = null;
            string emptyInput = "";

            // Act
            decimal? resultForNull = _textAnalysisService.ConvertToDecimal(nullInput);
            decimal? resultForEmpty = _textAnalysisService.ConvertToDecimal(emptyInput);

            // Assert
            Assert.IsNull(resultForNull);
            Assert.IsNull(resultForEmpty);
        }

        #endregion
    }
}
