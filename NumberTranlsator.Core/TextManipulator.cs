using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace NumberTranlsator.Core
{
    public class TextManipulator
    {
        private string filePath;
        private HashSet<string> sentenceEndPunctuationMarks;
        private HashSet<string> otherPunctuationMarks;

        public TextManipulator(string filePath)
        {
            this.filePath = filePath;
            this.sentenceEndPunctuationMarks = new HashSet<string> { ".", "!", "?"};
            this.otherPunctuationMarks = new HashSet<string> { "\\s", ",", ":", ";","\\(", "\\)", "\"", "'"};
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
            var textBuilder = new StringBuilder();
            int counter = 1;
            string prevWord = string.Empty;
            
            foreach (string word in words)
            {
                string modifiedWord = word;

                if (counter == 1 || this.sentenceEndPunctuationMarks.Contains(prevWord))
                    modifiedWord = word.FirstLetterToUpper();
                
                textBuilder.Append(modifiedWord);

                if(!string.IsNullOrWhiteSpace(word))
                    prevWord = word;

                counter++;
            }

            return textBuilder.ToString(); ;
        }

        private IEnumerable<string> GetWordsFromString(string text)
        {
            var regexBuilder = new StringBuilder();

            regexBuilder.Append("([");

            foreach (string delimiter in this.sentenceEndPunctuationMarks)
            {
                regexBuilder.Append(delimiter);
            }
            foreach (string delimiter in this.otherPunctuationMarks)
            {
                regexBuilder.Append(delimiter);
            }

            regexBuilder.Append("])");

            string pattern = regexBuilder.ToString();

            string[] words = Regex.Split(text, pattern);

            return words;
        }
    }
}
