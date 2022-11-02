using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public static class Class1
{
    public static bool[] bools = new bool[4];
    public static GameObject objects;
    public static bool Check()
    {
        foreach(var check in bools)
            if (!check) return false;
        return true;
    }
}
