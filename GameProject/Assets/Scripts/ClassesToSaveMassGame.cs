using System.Collections;
using System.IO;
using System.Linq;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class Ion: MonoBehaviour
{
    public string Name;
    public int DegreeOfOxide;
    public int NumberInTableOfDissolve;
    public int coef;
    public Ion(params string[] data)
    {
        Name = data[0];
        DegreeOfOxide = int.Parse(data[1]);
        NumberInTableOfDissolve = int.Parse(data[2]);
    }
    public bool IsIonComposite() => Name.Any(x => char.IsNumber(x));
    public override string ToString() => coef == 1 ? Name : IsIonComposite()
        ? $"({Name}){coef}"
        : Name + coef;
}
public class TableOfDissolve: MonoBehaviour
{
    public string[][] MetalIons;
    public string[][] OxideIons;
    public string[][] DissolvePanel;
    public TableOfDissolve()
    {
        MetalIons = File.ReadAllLines("Assets/Scripts/MetalIons.txt").Select(x => x.Split(' ')).ToArray();
        OxideIons = File.ReadAllLines("Assets/Scripts/OxideIons.txt").Select(x => x.Split(' ')).ToArray();
        DissolvePanel = File.ReadAllLines("Assets/Scripts/TableOfDissolve.txt").Select(x => x.Split(' ')).ToArray();
    }
}
public class Salt: MonoBehaviour
{
    public Ion metal;
    public Ion oxide;
    public double xCoef;
    public Salt(Ion met, Ion oxi)
    {
        metal = met;
        oxide = oxi;
        GetCoefs();
    }
    public void GetCoefs() => (metal.coef, oxide.coef) = metal.DegreeOfOxide == Math.Abs(oxide.DegreeOfOxide)
        ? (1, 1) : (Math.Abs(metal.DegreeOfOxide * oxide.DegreeOfOxide / metal.DegreeOfOxide), Math.Abs(metal.DegreeOfOxide * oxide.DegreeOfOxide / oxide.DegreeOfOxide));
    public bool SaltCondition(string[][] dissolvePanel, string condition)
        => dissolvePanel[oxide.NumberInTableOfDissolve][metal.NumberInTableOfDissolve] == condition ? true : false;
    public string ToString(string[][] dissolvePanel) => SaltCondition(dissolvePanel,"Í") ? metal.ToString() + oxide.ToString() + "\u2193" : metal.ToString()+oxide.ToString();
}
