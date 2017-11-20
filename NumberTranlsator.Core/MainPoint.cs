using System;
using System.Collections.Generic;

namespace NumberTranlsator.Core
{
    class MainPoint
    {
        static void Main(string[] args)
        {
            var textManipulator = new TextManipulator("Files\\SomeFile.txt");
            IEnumerable<string> textWords = textManipulator.GetWordsFromTex();

            var translatedWords = new List<string>();
            var translator = new Translator();

            foreach (var word in textWords)
            {
                string translatedWord = translator.TryTranslateNumber(word);
                translatedWords.Add(translatedWord);
            }

            string translatedText = textManipulator.GetTextFromWords(translatedWords);

            Console.WriteLine(translatedText);
        }
    }
}
