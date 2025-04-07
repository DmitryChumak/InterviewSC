using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Task1
{
    /// <summary>
    /// The WordsCounter class processes a collection of text files and counts the frequency of each word.
    /// </summary>
    public class WordsCounter
    {
        private readonly string[] _filePaths;
        private readonly ConcurrentDictionary<string, int> _wordsDictionary = new ConcurrentDictionary<string, int>();
        private bool _isProcessed;

        /// <summary>
        /// Initializes a new instance of the WordsCounter class with the given file paths.
        /// </summary>
        /// <param name="filePaths">An array of file paths to be processed.</param>
        public WordsCounter(string[] filePaths)
        {
            _filePaths = filePaths;
        }

        public async Task<Dictionary<string, int>> GetWordsDictionary(bool cached = true)
        {
            _isProcessed = cached;
            await CountWords();

            return _wordsDictionary.ToDictionary(item => item.Key, item => item.Value);
        }

        private void ProcessLine(string line)
        {
            line = line.ToLower();
            char[] delimiters = { ' ', '\t', '\r', '\n', '.', ',', '!', '?', ';', ':' };

            string[] words = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                _wordsDictionary.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
            }
        }

        private async Task ProcessFiles()
        {
            IEnumerable<Task> tasks = _filePaths.Select(file => Task.Run(() => ProcessFile(file)));
            await Task.WhenAll(tasks);
        }

        private void ProcessFile(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            ProcessLine(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file '{file}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"File '{file}' not found.");
            }
        }

        private async Task CountWords()
        {
            if (!_isProcessed)
            {
                await ProcessFiles();
                _isProcessed = true;
            }
        }
    }
}
