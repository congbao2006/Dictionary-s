using Dicktionary.Models;
using Dicktionary.Data;

namespace Dicktionary.Services
{
    public static class SynAntDictionary
    {
        public static void LoadFromFile(string path)
        {
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('|', StringSplitOptions.TrimEntries);

                string word = parts[0].ToLower();

                List<string> synList = new();
                List<string> antList = new();

                // Đồng nghĩa
                if (parts.Length >= 2 && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    synList = parts[1]
                        .Split(',', StringSplitOptions.TrimEntries)
                        .Where(x => x != "")
                        .Select(x => x.ToLower())
                        .ToList();
                }

                // Trái nghĩa
                if (parts.Length >= 3 && !string.IsNullOrWhiteSpace(parts[2]))
                {
                    antList = parts[2]
                        .Split(',', StringSplitOptions.TrimEntries)
                        .Where(x => x != "")
                        .Select(x => x.ToLower())
                        .ToList();
                }

                SynAntMeaning.Synonyms[word] = synList;
                SynAntMeaning.Antonyms[word] = antList;
            }
        }

        public static string SearchSyn(string word)
        {
            word = word.ToLower();

            if (SynAntMeaning.Synonyms.ContainsKey(word) &&
                SynAntMeaning.Synonyms[word].Count > 0)
                return string.Join(", ", SynAntMeaning.Synonyms[word]);

            return "notfound";
        }

        public static string SearchAnt(string word)
        {
            word = word.ToLower();

            if (SynAntMeaning.Antonyms.ContainsKey(word) &&
                SynAntMeaning.Antonyms[word].Count > 0)
                return string.Join(", ", SynAntMeaning.Antonyms[word]);

            return "notfound";
        }

        public static bool AddSyn(string word, string synonym)
        {
            word = word.ToLower();
            synonym = synonym.ToLower();

            // Nếu chưa có thì tạo list mới
            if (!SynAntMeaning.Synonyms.ContainsKey(word))
                SynAntMeaning.Synonyms[word] = new List<string>();

            // Tránh trùng lặp
            if (SynAntMeaning.Synonyms[word].Contains(synonym))
                return false;

            SynAntMeaning.Synonyms[word].Add(synonym);
            return true;
        }

        public static bool AddAnt(string word, string antonym)
        {
            word = word.ToLower();
            antonym = antonym.ToLower();

            if (!SynAntMeaning.Antonyms.ContainsKey(word))
                SynAntMeaning.Antonyms[word] = new List<string>();

            if (SynAntMeaning.Antonyms[word].Contains(antonym))
                return false;

            SynAntMeaning.Antonyms[word].Add(antonym);
            return true;
        }


    }
}
