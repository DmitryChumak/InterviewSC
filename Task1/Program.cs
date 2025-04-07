using System;
using System.Threading.Tasks;

namespace Task1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string[] files = { @"C:\Repos\file1.txt", @"C:\Repos\file2.txt" };

            var wordsCounter = new WordsCounter(files);
            var wordsDictionary = await wordsCounter.GetWordsDictionary();

            foreach (var word in wordsDictionary)
            {
                Console.WriteLine($"{word.Value}: {word.Key}");
            }
        }
    }
}
