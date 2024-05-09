using UnityEngine;
using System;

public class Calendar : MonoBehaviour
{
    private int currentMonth = DateTime.Now.Month;
    private int currentYear = DateTime.Now.Year;
    private int currentDay = DateTime.Now.Day;
    private const int CELL_WIDTH = 50;
    private const int CELL_HEIGHT = 50;

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 30), $"当前月份：{currentYear}年 {currentMonth}月 {currentDay}日");

        if (GUI.Button(new Rect(10, 50, 100, 30), "上月"))
        {
            if (currentMonth == 1)
            {
                currentMonth = 12;
                currentYear--;
            }
            else
            {
                currentMonth--;
            }
        }

        if (GUI.Button(new Rect(120, 50, 100, 30), "下月"))
        {
            if (currentMonth == 12)
            {
                currentMonth = 1;
                currentYear++;
            }
            else
            {
                currentMonth++;
            }
        }
    }
}
