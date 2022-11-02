using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public static class EnumsAndRandomiser
{
    public static List<string> elements = File.ReadAllLines("Assets/Scripts/Elements.txt").ToList();
    public static List<string> elementsRus = File.ReadAllLines("Assets/Scripts/ElementsRus.txt").ToList();
    public static Dictionary<string,string> GetDict(List<string> keys, List<string> values)
    { 
        Dictionary<string,string> dict = new Dictionary<string,string>();
        for (int i = 0; i < keys.Count; i++)
            dict.Add(keys[i], values[i]);
        return dict;
    }
    public static Dictionary<string, string> elementsDictionary = GetDict(elements,elementsRus);
    public static List<string> elementsInGame = new List<string>();
}
