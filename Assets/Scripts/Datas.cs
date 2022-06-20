using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datas
{

}

[Serializable]
public struct Subject
{
    public List<string> SubjectInfo;
}

[Serializable]
public struct Subjects
{
    public List<Subject> subjects;
}
