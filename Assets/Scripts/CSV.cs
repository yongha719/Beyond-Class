using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public static class CSV
{
    //public static LoadType Load<LoadType>(string name)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    var data = Resources.Load(name) as TextAsset;
    //    LoadType type
    //    Json.Save(data, "CSVtest");
    //    return 
    //    //return 
    //}
    public static void Load(string name)
    {
        StringBuilder sb = new StringBuilder();
        var data = Resources.Load(name) as TextAsset;
        Json.Save(data, "CSVtest");
    }
}
