using Dictionary.Models;
using Dictionary.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dictionary.Services
{
    public static class SynAntDictionary
    {
        // ---------------------------
        // LOAD FILE
        // ---------------------------
        public static void LoadFromFile(string path)
        {
            var lines = FileHelper.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('|');

                string word = parts[0].Trim().ToLower();

                List<string> synList = new();
                List<string> antList = new();

                if (parts.Length >= 2)
                {
                    synList = parts[1]
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim().ToLower())
                        .Where(x => x != "")
                        .ToList();
                }

                if (parts.Length >= 3)
                {
                    antList = parts[2]
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim().ToLower())
                        .Where(x => x != "")
                        .ToList();
                }

                SynAntMeaning.Synonyms[word] = synList;
                SynAntMeaning.Antonyms[word] = antList;
            }
        }

        // ---------------------------
        // UPDATE ONE LINE
        // ---------------------------
        private static void UpdateLine(string path, string word)
        {
            word = word.ToLower();
            var lines = FileHelper.ReadAllLines(path);
            bool found = false;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split('|');
                if (parts[0].Trim().ToLower() != word) continue;

                string syn = SynAntMeaning.Synonyms.ContainsKey(word)
                    ? string.Join(",", SynAntMeaning.Synonyms[word])
                    : "";

                string ant = SynAntMeaning.Antonyms.ContainsKey(word)
                    ? string.Join(",", SynAntMeaning.Antonyms[word])
                    : "";

                // GIỮ NGUYÊN FORMAT: word|syn1, syn2|ant1, ant2
                lines[i] = $"{word}|{syn}|{ant}";
                found = true;
                break;
            }

            if (!found)
            {
                string syn = SynAntMeaning.Synonyms.ContainsKey(word)
                    ? string.Join(",", SynAntMeaning.Synonyms[word])
                    : "";

                string ant = SynAntMeaning.Antonyms.ContainsKey(word)
                    ? string.Join(",", SynAntMeaning.Antonyms[word])
                    : "";

                lines.Add($"{word}|{syn}|{ant}");
            }

            FileHelper.WriteAllLines(path, lines);
        }

        // ---------------------------
        // ADD SYN
        // ---------------------------
        public static string AddSyn(string path, string word, string synonym)
    {
        word = word.ToLower();
        synonym = synonym.ToLower();

        var lines = FileHelper.ReadAllLines(path);
        bool found = false;

        for (int i = 0; i < lines.Count; i++)
        {
            string[] parts = lines[i].Split('|');

            if (parts[0].Trim().ToLower() == word)
            {
                // Lấy danh sách syn cũ
                List<string> synList = new();

                if (parts.Length >= 2 && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    synList = parts[1]
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim().ToLower())
                        .ToList();
                }

                // Nếu chưa có thì thêm
                if (!synList.Contains(synonym))
                    synList.Add(synonym);

                // Lấy lại antonym cũ
                string ant = parts.Length >= 3 ? parts[2].Trim() : "";

                // Ghi lại dòng mới
                string syn = string.Join(", ", synList);
                lines[i] = $"{word}|{syn}|{ant}";

                found = true;
                break;
        }
    }

        // Nếu từ chưa có → thêm dòng mới
        if (!found)
        {
            lines.Add($"{word}|{synonym}|");
        }

        FileHelper.WriteAllLines(path, lines);

        return "success";
    }


        // ---------------------------
        // ADD ANT
        // ---------------------------
        public static string AddAnt(string path, string word, string antonym)
        {
            word = word.ToLower();
            antonym = antonym.ToLower();

            var lines = FileHelper.ReadAllLines(path);
            bool found = false;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split('|');

                if (parts[0].Trim().ToLower() == word)
                {
                    // Lấy danh sách ant cũ
                    List<string> antList = new();

                    if (parts.Length >= 3 && !string.IsNullOrWhiteSpace(parts[2]))
                    {
                        antList = parts[2]
                            .Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Trim().ToLower())
                            .ToList();
                    }

                    // Nếu chưa có thì thêm
                    if (!antList.Contains(antonym))
                        antList.Add(antonym);

                    // Lấy lại synonym cũ
                    string syn = parts.Length >= 2 ? parts[1].Trim() : "";

                    // Ghi lại dòng mới
                    string ant = string.Join(", ", antList);
                    lines[i] = $"{word}|{syn}|{ant}";

                    found = true;
                    break;
                }
            }

            // Nếu từ chưa tồn tại => tạo dòng mới
            if (!found)
            {
                lines.Add($"{word}||{antonym}");
            }

            FileHelper.WriteAllLines(path, lines);

            return "success";
        }


        // ---------------------------
        // SEARCH
        // ---------------------------
        public static string SearchSyn(string word)
        {
            word = word.ToLower();
            if (SynAntMeaning.Synonyms.ContainsKey(word))
                return string.Join(", ", SynAntMeaning.Synonyms[word]);
            return "notfound";
        }

        public static string SearchAnt(string word)
        {
            word = word.ToLower();
            if (SynAntMeaning.Antonyms.ContainsKey(word))
                return string.Join(", ", SynAntMeaning.Antonyms[word]);
            return "notfound";
        }


        public static void SaveToFile(string path)
        {
            List<string> lines = new List<string>();

            foreach (var kv in SynAntMeaning.Synonyms)
            {
                string word = kv.Key;

                string syn = SynAntMeaning.Synonyms.ContainsKey(word)
                    ? string.Join(", ", SynAntMeaning.Synonyms[word])
                    : "";

                string ant = SynAntMeaning.Antonyms.ContainsKey(word)
                    ? string.Join(", ", SynAntMeaning.Antonyms[word])
                    : "";

                lines.Add($"{word}|{syn}|{ant}");
            }

            File.WriteAllLines(path, lines);
        }

        // ---------------------------
        // DELETE SYN
        // ---------------------------


        public static bool DeleteSyn(string filePath, string word, string synonym)
        {
            word = word.ToLower();
            synonym = synonym.ToLower();

            if (!SynAntMeaning.Synonyms.ContainsKey(word))
                return false;

            bool removed = SynAntMeaning.Synonyms[word].Remove(synonym);

            if (removed)
                SaveToFile(filePath);

            return removed;
        }

        // ---------------------------
        // DELETE ANT
        // ---------------------------

        public static bool DeleteAnt(string filePath, string word, string antonym)
        {
            word = word.ToLower();
            antonym = antonym.ToLower();

            if (!SynAntMeaning.Antonyms.ContainsKey(word))
                return false;

            bool removed = SynAntMeaning.Antonyms[word].Remove(antonym);

            if (removed)
                SaveToFile(filePath);

            return removed;
        }


    }
}
