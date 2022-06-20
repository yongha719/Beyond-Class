using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ClassManager : MonoBehaviour
{
    [SerializeField] Transform SubjectParent;
    [SerializeField] GameObject SubjectPrefab;

    [SerializeField] Text TodayDate;

    /// <summary>
    /// 6교시인 날인 경우 꺼줌
    /// </summary>
    [SerializeField] GameObject SeventhClass;

    Subject Subject;
    int daynum;
    void Start()
    {
        #region Date Set
        DateTime today = DateTime.Today;

        DateTime nowDt = DateTime.Now;

        string dotw = "";

        switch (nowDt.DayOfWeek)
        {
            case DayOfWeek.Monday:
                dotw = "월요일";
                break;
            case DayOfWeek.Tuesday:
                dotw = "화요일";
                break;
            case DayOfWeek.Wednesday:
                dotw = "수요일";
                break;
            case DayOfWeek.Thursday:
                dotw = "목요일";
                break;
            case DayOfWeek.Friday:
                dotw = "금요일";
                break;
            case DayOfWeek.Saturday:
                dotw = "토요일";
                break;
            case DayOfWeek.Sunday:
                dotw = "일요일";
                break;
            default:
                break;
        }

        TodayDate.text = $@"{today.Month} / {today.Day} {dotw}";
        #endregion

        var subjects = Json.LoadList<Subject>("Subject");

        Subject = subjects[0];
        int daynum = (int)nowDt.DayOfWeek;


        if (Input.GetKeyDown(KeyCode.S))
        {
            daynum++;
            if (daynum == (int)DayOfWeek.Wednesday)
            {
                SeventhClass.SetActive(false);
                for (int i = 0; i < 6; i++)
                {
                    //Text text = Instantiate(Subject, SubjectParent).GetComponent<Text>();

                    //text.text = [daynum].dad[i];
                }
            }

        }


    }

    void Update()
    {

    }
}
