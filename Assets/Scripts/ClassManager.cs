using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public struct Test
{
    public List<string> names;
}
[Serializable]
public struct Tests
{
    public List<Test> tests;
}
public class ClassManager : MonoBehaviour
{
    const string DAY = "Day";
    [SerializeField] Transform SubjectParent;
    [SerializeField] GameObject SubjectPrefab;

    [SerializeField] Text TodayDate;

    Test test;
    Tests tests;
    /// <summary>
    /// 6교시인 날인 경우 꺼줌
    /// </summary>
    [SerializeField] GameObject SeventhClassobj;
    public List<Subject> subjects;
    int daynum;

    void Start()
    {
        #region Date Set
        DateTime today = DateTime.Today;

        DateTime nowDt = DateTime.Now;

        string dayofweek = "";

        switch (nowDt.DayOfWeek)
        {
            case DayOfWeek.Monday:
                dayofweek = "월요일";
                break;
            case DayOfWeek.Tuesday:
                dayofweek = "화요일";
                break;
            case DayOfWeek.Wednesday:
                dayofweek = "수요일";
                break;
            case DayOfWeek.Thursday:
                dayofweek = "목요일";
                break;
            case DayOfWeek.Friday:
                dayofweek = "금요일";
                break;
            case DayOfWeek.Saturday:
                dayofweek = "토요일";
                break;
            case DayOfWeek.Sunday:
                dayofweek = "일요일";
                break;
            default:
                break;
        }

        TodayDate.text = $@"{today.Month} / {today.Day} {dayofweek}";
        #endregion

        subjects = Json.LoadList<Subject>("Subject");

        daynum = (int)nowDt.DayOfWeek;

        if (daynum == (int)DayOfWeek.Wednesday)
        {
            SeventhClassobj.SetActive(false);
            for (int i = 0; i < 6; i++)
            {
                Text text = Instantiate(SubjectPrefab, SubjectParent).GetComponentInChildren<Text>();

                text.text = subjects[daynum - 1].SubjectInfo[i];
            }
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                Text text = Instantiate(SubjectPrefab, SubjectParent).GetComponentInChildren<Text>();

                text.text = subjects[daynum - 1].SubjectInfo[i];
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            daynum++;
            daynum = Mathf.Clamp(daynum, 1, 6);
            if (daynum == (int)DayOfWeek.Wednesday)
            {
                SeventhClassobj.SetActive(false);
                for (int i = 0; i < 6; i++)
                {
                    Text text = Instantiate(SubjectPrefab, SubjectParent).GetComponentInChildren<Text>();

                    text.text = subjects[daynum - 1].SubjectInfo[i];
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    Text text = Instantiate(SubjectPrefab, SubjectParent).GetComponentInChildren<Text>();

                    text.text = subjects[daynum - 1].SubjectInfo[i];
                }
            }

        }

    }

    void OnApplicationQuit()
    {
        print("d");
    }
}
