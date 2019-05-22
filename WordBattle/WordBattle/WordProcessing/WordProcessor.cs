using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WordBattle.WordProcessing
{
    public static class WordProcessor
    {
        static HashSet<string> Words = new HashSet<string>();
        static Random random = new Random();

        public static void Initialize()
        {
            if (Words.Count == 0)
            {            
                LoadWords();
            }
        }

        public static int Count()
        {
            return Words.Count;
        }

        public static bool IsValid(string word, string startLetter)
        {
            return Words.Contains(word) && word.StartsWith(startLetter);
        }

        public static string GetWord()
        {    
            int index = random.Next(Words.Count);

            string word = Words.ElementAt(index);

            if (word.Length >= 14)
            {
                word = "..." + word.Substring(word.Length - 14, 11);
            }

            return word;
        }

        public static string GetWord(string startLetter)
        {
            var list = Words.Where(r => r.StartsWith(startLetter));
            
            int i = random.Next(list.Count());

            string word = list.ElementAt(i);

            if (word.Length >= 14)
            {
                word = "..." + word.Substring(word.Length - 14, 11);
            }

            return word;
        }

        static void LoadWords()
        {
            string word;

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("WordBattle.Words.txt");

            // Read the file and display it line by line.  
            using (StreamReader file = new StreamReader(stream))
            {
                while ((word = file.ReadLine()) != null)
                {
                    Words.Add(word);
                }
            }
        }
    }
}