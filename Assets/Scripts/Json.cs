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
    public static void Save<SaveType>(SaveType writeType, string filename)
    {
        string json = JsonUtility.ToJson(writeType);
        File.WriteAllText($"{Application.dataPath}/Resources/{filename}.json", json);
    }

    public static LoadType Load<LoadType>(string filename)
    {
        TextAsset txt = Resources.Load<TextAsset>(filename);
        return JsonUtility.FromJson<LoadType>(txt.text);
    }

    public static List<LoadType> LoadList<LoadType>(string filename)
    {
        TextAsset txt = Resources.Load<TextAsset>(filename);
        return JsonUtility.FromJson<Serialization<LoadType>>(txt.text).target;
    }
}
