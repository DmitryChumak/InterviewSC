using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;

namespace Tests
{
    [TestClass]
    public class Task1Tests
    {
        private string[] _testFilePaths;

        [TestInitialize]
        public void TestInitialize()
        {
            string testDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            _testFilePaths = new[] { Path.Combine(testDirectory, "file1.txt"), Path.Combine(testDirectory, "file2.txt") };
        }

        [TestMethod]
        public async Task GetWordsDictionary_ShouldReturnCorrectWordCounts_WhenFilesAreProcessed()
        {
            // Arrange
            var wordsCounter = new WordsCounter(_testFilePaths);

            // Act
            var wordsDictionary = await wordsCounter.GetWordsDictionary(false);

            // Assert
            Assert.AreEqual(10, wordsDictionary.Count); 
            Assert.AreEqual(1, wordsDictionary["go"]); 
            Assert.AreEqual(2, wordsDictionary["do"]); 
            Assert.AreEqual(2, wordsDictionary["that"]); 
            Assert.AreEqual(1, wordsDictionary["thing"]); 
            Assert.AreEqual(1, wordsDictionary["you"]); 
            Assert.AreEqual(1, wordsDictionary["so"]);
            Assert.AreEqual(2, wordsDictionary["well"]); 
            Assert.AreEqual(1, wordsDictionary["i"]); 
            Assert.AreEqual(1, wordsDictionary["play"]);
            Assert.AreEqual(1, wordsDictionary["football"]); 
        }

        [TestMethod]
        public async Task GetWordsDictionary_ShouldReturnEmpty_WhenFilesAreEmpty()
        {
            // Arrange
            string emptyFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "emptyFile.txt");
            File.WriteAllText(emptyFilePath, string.Empty);

            var wordsCounter = new WordsCounter(new[] { emptyFilePath });

            // Act
            var wordsDictionary = await wordsCounter.GetWordsDictionary(false);

            // Assert
            Assert.AreEqual(0, wordsDictionary.Count);

            File.Delete(emptyFilePath);
        }

        [TestMethod]
        public async Task GetWordsDictionary_ShouldHandleMultipleOccurrencesOfTheSameWord()
        {
            // Arrange
            string multipleWordsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "multipleWordsFile.txt");
            File.WriteAllText(multipleWordsFilePath, "hello hello hello hello");

            var wordsCounter = new WordsCounter(new[] { multipleWordsFilePath });

            // Act
            var wordsDictionary = await wordsCounter.GetWordsDictionary(false);

            // Assert
            Assert.AreEqual(3, wordsDictionary["hello"]);

            File.Delete(multipleWordsFilePath);
        }

        [TestMethod]
        public async Task GetWordsDictionary_ShouldReturnEmpty_WhenNoFilesExist()
        {
            // Arrange
            var nonExistentFiles = new[] { "nonexistentfile1.txt", "nonexistentfile2.txt" };
            var wordsCounter = new WordsCounter(nonExistentFiles);

            // Act
            var wordsDictionary = await wordsCounter.GetWordsDictionary(false);

            // Assert
            Assert.AreEqual(0, wordsDictionary.Count);
        }
    }
}
