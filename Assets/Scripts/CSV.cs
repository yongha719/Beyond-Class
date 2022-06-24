using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public static class CSV
{
    public static LoadType Load<LoadType>(string name)
    {
        StringBuilder sb = new StringBuilder();
        var data = Resources.Load(name) as TextAsset;


        // return 
    }
}
