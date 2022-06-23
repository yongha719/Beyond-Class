using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public List<Subject> subjects;

    [SerializeField] Transform SubjectParent;
    [SerializeField] GameObject SubjectPrefab;

    [SerializeField] Text TodayDate;

    /// <summary>
    /// 6교시인 날인 경우 꺼줌
    /// </summary>
    [SerializeField] GameObject SeventhClassobj;
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

        // Set TimeTable
        subjects = Json.LoadList<Subject>("Subject");

        daynum = (int)nowDt.DayOfWeek;

        Text subjecttext;

        if (daynum == (int)DayOfWeek.Wednesday)
        {
            SeventhClassobj.SetActive(false);
            for (int i = 0; i < 6; i++)
            {
                subjecttext = Instantiate(SubjectPrefab, SubjectParent).GetComponentInChildren<Text>();

                subjecttext.text = subjects[daynum - 1].SubjectInfo[i];
            }
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                subjecttext = Instantiate(SubjectPrefab, SubjectParent).GetComponentInChildren<Text>();

                subjecttext.text = subjects[daynum - 1].SubjectInfo[i];
            }
        }
    }

    void OnApplicationQuit()
    {
        print("d");
    }
}
