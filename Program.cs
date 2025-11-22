using System;
using Dicktionary.Services;

class Program
{
    static void Main()
    {
        Console.Clear();
        string path = "/Users/bao/Desktop/Dicktionary-main/base/Data/FavoriteWordData.txt";
        DictionaryMyFavorite.AddFavorite(path, "apple");
        
    }
}
