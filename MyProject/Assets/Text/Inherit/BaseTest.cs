using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTest
{
    public virtual void Test1()
    {
        EventManage.Ins.RegisterEvent("BaseTest1", Event);
    }
    public virtual void Test2()
    {
        EventManage.Ins.RegisterEvent("BaseTest2", Event);
    }
    private void Event()
    {
        Debug.Log("BaseTest         /////");
    }
    private void DelEvent()
    {
        EventManage.Ins.DelEvent("BaseTest1");
    }
}
