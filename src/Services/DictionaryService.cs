using System;
using System.Collections.Generic;
using System.Linq;
using Dictionary.Models;
using Dictionary.Data;

namespace Dictionary.Services
{
    public static class DictionaryService
    {
        public static Dictionary<string, Meaning> dictionary = new Dictionary<string, Meaning>();
        public static bool forSort = false;

        public static void Add(string word, string definition, string description, string example)
        {
            if (dictionary.ContainsKey(word))
                return;

            dictionary[word] = new Meaning(definition, description, example);
        }

        public static void Remove(string word)
        {
            if (dictionary.ContainsKey(word))
                dictionary.Remove(word);
        }

        // -------------------------
        //  PRINT LIST OF WORDS
        // -------------------------
        public static string PrintListOfWord()
        {
            if (dictionary.Count == 0)
            {
                return "null";
            }

            string result = "List of words:\n";
            result += new string('-', 40) + "\n";

            foreach (var entry in dictionary)
            {
                result += entry.Key + "\n";
            }

            return result;
        }

        // -------------------------
        //  SEARCH WORD
        // -------------------------
        public static string Search(string word)
        {
            if (!dictionary.ContainsKey(word))
            {
                return $"not found {word}";
            }

            var meaning = dictionary[word];

            return
                $"Từ: {word}\n" +
                $"Nghĩa: {meaning.Definition}\n" +
                $"Mô tả: {meaning.Description}\n" +
                $"Ví dụ: {meaning.Example}";
        }

        // -------------------------
        // LOAD FILE
        // -------------------------
        public static string LoadFromFile(string path)
        {
            var lines = FileHelper.ReadAllLines(path);
            int lineIndex = 0;

            foreach (var line in lines)
            {
                lineIndex++;
                var parts = line.Split('|');
                if (parts.Length < 4) continue;

                Add(parts[0], parts[1], parts[2], parts[3]);
            }

            if (lineIndex == 0)
                return "Not found any data";

            if (!forSort)
                return $"Load success {lineIndex + 1} word";

            return "Sorted successfully";
        }

        // -------------------------
        // ADD TO FILE
        // -------------------------
        public static string AddToFile(string path, string word, string definition, string description, string example)
        {
            if (dictionary.ContainsKey(word))
            {
                return $"{word} exists, cannot add";
            }

            string line = $"{word}|{definition}|{description}|{example}";

            try
            {
                FileHelper.AppendLine(path, line);
                dictionary[word] = new Meaning(definition, description, example);
                return "Saved successfully";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // -------------------------
        // DELETE WORD FROM FILE
        // -------------------------
        public static string DeleteFromFile(string path, string word)
        {
            var lines = FileHelper.ReadAllLines(path);

            var newLines = lines
                .Where(line => !line.StartsWith(word + "|", StringComparison.OrdinalIgnoreCase))
                .ToList();

            FileHelper.WriteAllLines(path, newLines);

            if (dictionary.ContainsKey(word))
            {
                dictionary.Remove(word);
                return $"Deleted '{word}' from file successfully.";
            }

            return $"{word} not found in file";
        }

        // -------------------------
        // SORT FILE
        // -------------------------
        public static string SortFile(string path)
        {
            var lines = FileHelper.ReadAllLines(path);
            forSort = true;

            lines = lines
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .OrderBy(l => l.Split('|')[0], StringComparer.OrdinalIgnoreCase)
                .ToList();

            FileHelper.WriteAllLines(path, lines);

            dictionary.Clear();
            return LoadFromFile(path);
        }
    }
}
