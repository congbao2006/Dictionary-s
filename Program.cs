using System;
using Dicktionary.Services;

class Program
{
    static void Main()
    {
        Console.Clear();
        //string path = "/Users/bao/Desktop/Dicktionary-main/base/Data/MeaningWordData.txt";
        string synAntPath = "/Users/bao/Desktop/Dicktionary-main/base/Data/SynAntWordData.txt";
        SynAntDictionary.LoadFromFile(synAntPath);
        Console.WriteLine(SynAntDictionary.SearchSyn("happy"));
        //DicktionaryService.LoadFromFile(path);
        //DicktionaryService.PrintListOfWord();
        //DicktionaryService.Search("apple");
        //DicktionaryService.DeleteFromFile(path, "apple");
        //DicktionaryService.AddToFile(path,"apple","quả táo","A sweet red or green fruit","I like to eat an apple every day.");
        //DicktionaryService.Search("water");
        //DicktionaryService.SortFile(path);
        
    }
}
