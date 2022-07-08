using System;
using System.Collections;
using System.Collections.Generic;

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
