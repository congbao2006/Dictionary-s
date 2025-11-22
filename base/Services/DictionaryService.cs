using System;
using System.Collections.Generic;
using System.Linq;
using Dicktionary.Models;
using Dicktionary.Data;

namespace Dicktionary.Services
{
    public static class DicktionaryService
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

        public static void PrintListOfWord()
        {
            if (dictionary.Count == 0)
            {
                Console.WriteLine("null");
                return;
            }

            Console.WriteLine("List of words:");
            Console.WriteLine(new string('-', 40));

            foreach (var entry in dictionary)
            {
                Console.WriteLine(entry.Key);
            }
        }

         public static void Search(string word)
        {
            if (!dictionary.ContainsKey(word))
            {
                Console.WriteLine($"not found {word}");
                return;
            }
            var meaning = dictionary[word];
            Console.WriteLine($"Từ: {word}");
            Console.WriteLine($"Nghĩa: {meaning.Definition}");
            Console.WriteLine($"Mô tả: {meaning.Description}");
            Console.WriteLine($"Ví dụ: {meaning.Example}");
        }

        public static void LoadFromFile(string path)
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
            Console.WriteLine("Not found any data");
        else
            if (!forSort) Console.WriteLine($"Load success {lineIndex+1} word");
        else 
            Console.WriteLine("Sorted successfully");
        }

        public static void AddToFile(string path, string word, string definition, string description, string example)
        {
            if (dictionary.ContainsKey(word))
            {
                Console.WriteLine($"{word} exists, cannot add");
                return;
            }

            string line = $"{word}|{definition}|{description}|{example}";

            try
            {
                FileHelper.AppendLine(path, line);
                Console.WriteLine("Saved successfully");

                dictionary[word] = new Meaning(definition, description, example);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        public static void DeleteFromFile(string path, string word)
        {
            var lines = FileHelper.ReadAllLines(path);

            var newLines = lines
                .Where(line => !line.StartsWith(word + "|", StringComparison.OrdinalIgnoreCase))
                .ToList();

            FileHelper.WriteAllLines(path, newLines);

            if (dictionary.ContainsKey(word))
            {
                dictionary.Remove(word);
                Console.WriteLine($"Deleted '{word}' from file successfully.");
            }
            else
                Console.WriteLine($"{word} not found in file");
            
        }
        public static void SortFile(string path)
        {
            var lines = FileHelper.ReadAllLines(path);
            forSort = true;

            lines = lines
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .OrderBy(l => l.Split('|')[0], StringComparer.OrdinalIgnoreCase)
                .ToList();

            FileHelper.WriteAllLines(path, lines);

            dictionary.Clear();
            LoadFromFile(path);
        }
    }
}
