using System;
using Dicktionary.Services;

class Program
{
    static void Main()
    {
        Console.Clear();
        string synAntFilePath = "/Users/bao/Desktop/Dicktionary-main/base/Data/SynAntWordData.txt";
        SynAntDictionary.AddAnt(synAntFilePath,"sad", "glad");
    }
}
