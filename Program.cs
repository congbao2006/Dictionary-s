using System;
using Dictionary.Services;

class Program
{
    static void Main()
    {
        Console.Clear();
        string path = "/Users/bao/Desktop/Dictionary-main/src/Data/MeaningWordData.txt";
        DictionaryService.LoadFromFile(path);
        //DictionaryService.AddToFile(path,"example", "a representative form or pattern", "This is a description.", "This is an example sentence.");
        //Console.WriteLine(DictionaryService.Search("example"));
        DictionaryService.SortFile(path);
    }
}
