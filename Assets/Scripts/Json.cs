using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Serialization<T>
{
    public Serialization(List<T> list) => target = list;
    public List<T> target;
}
public static class Json
{
    const string Path = @"Assets/Resources/";
    public static void Write<WriteType>(WriteType writeType, string name)
    {
        string json = JsonUtility.ToJson(writeType);
        File.WriteAllText(Path + name + ".json", json);
    }

    public static LoadType Load<LoadType>(string name)
    {
        TextAsset txt = Resources.Load<TextAsset>(name);
        return JsonUtility.FromJson<LoadType>(txt.text);
    }

    public static List<LoadType> LoadList<LoadType>(string name)
    {
        TextAsset txt = Resources.Load<TextAsset>(name);
        return JsonUtility.FromJson<Serialization<LoadType>>(txt.text).target;
    }
}
