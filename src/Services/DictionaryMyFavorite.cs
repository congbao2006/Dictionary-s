using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dictionary.Services
{
    public static class DictionaryMyFavorite
    {
        public static void AddFavorite(string path, string word)
        {
            word = word.Trim().ToLower();

            List<string> lines = new List<string>();
            if (File.Exists(path))
                lines = File.ReadAllLines(path).ToList();

            if (!lines.Contains(word))
            {
                lines.Add(word);
                File.WriteAllLines(path, lines);
            }
        }

        public static void RemoveFavorite(string path, string word)
        {
            word = word.Trim().ToLower();

            if (!File.Exists(path)) return;

            var lines = File.ReadAllLines(path).ToList();
            if (lines.Contains(word))
            {
                lines.Remove(word);
                File.WriteAllLines(path, lines);
            }
        }

    }
}
