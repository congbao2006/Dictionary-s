using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Dictionary.Models;

namespace Dictionary.Data
{
    public static class FileHelper
    {
        public static List<string> ReadAllLines(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            return File.ReadAllLines(path).ToList();
        }

        public static void WriteAllLines(string path, List<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        public static void AppendLine(string path, string line)
        {
            using (var sw = new StreamWriter(path, true))
            {
                sw.WriteLine(line);
            }
        }
    }
}
