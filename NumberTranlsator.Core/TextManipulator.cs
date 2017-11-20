using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NumberTranlsator.Core
{
    public class TextManipulator
    {
        private string filePath;
        private char[] wordsDelimiters;

        public TextManipulator(string filePath)
        {
            this.filePath = filePath;
            this.wordsDelimiters = new char[] { ' ', ',', '.', '!', '?', '-', ':', ';', '(', ')', '"', '\'' };
        }

        public IEnumerable<string> GetWordsFromTex()
        {
            List<string> words = new List<string>();

            using (StreamReader reader = new StreamReader(this.filePath, Encoding.UTF8))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    IEnumerable<string> lineWords = GetWordsFromString(line);

                    words.AddRange(lineWords);

                    line = reader.ReadLine();
                }
            }

            return words;
        }
        public string GetTextFromWords(IEnumerable<string> words)
        {
            string text = string.Join(' ', words);

            return text;
        }

        private IEnumerable<string> GetWordsFromString(string text)
        {
            string[] words = text.Split(this.wordsDelimiters, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
    }
}
