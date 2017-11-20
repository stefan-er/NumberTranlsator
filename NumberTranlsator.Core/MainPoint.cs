using System;
using System.Collections.Generic;

namespace NumberTranlsator.Core
{
    class MainPoint
    {
        static void Main(string[] args)
        {
            var textManipulator = new TextManipulator("Files\\SomeFile.txt");
            var translator = new Translator();

            IEnumerable<string> textWords = textManipulator.GetWordsFromTex();
            var translatedWords = new List<string>();

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
