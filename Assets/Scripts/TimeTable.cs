using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTable : MonoBehaviour
{
    private List<Subject> subjects;

    [SerializeField] private Transform SubjectParent;
    [SerializeField] private GameObject SubjectPrefab;

    [SerializeField] private Text TodayDate;

    /// <summary>
    /// 6교시인 날인 경우 꺼줌
    /// </summary>
    [SerializeField] private GameObject SeventhClassobj;
    int daynum;
    public Subjects testsubjects;

    void Start()
    {
        //TODO - 2022-07-08 박용하
        //하드코딩한거 다 바꾸기
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

        //TodayDate.text = $@"{today.Month} / {today.Day} {dayofweek}";
        #endregion

        // Set TimeTable
        subjects = Json.LoadList<Subject>("Subject");
        daynum = (int)nowDt.DayOfWeek;

        var subjectslist = Json.LoadList<Subjects>("Subjects");

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
