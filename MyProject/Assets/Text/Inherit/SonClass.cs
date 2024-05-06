using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonClass : BaseTest
{
    public override void Test1()
    {
        base.Test1();
        EventManage.Ins.RegisterEvent("SonClass1", Event2);
    }
    public override void Test2()
    {
        base.Test1();
        EventManage.Ins.RegisterEvent("SonClass2", Event2);
    }
    private void Event2()
    {
        Debug.Log("SonClass         /////");
    }
    private void DelEvent()
    {
        EventManage.Ins.DelEvent("SonClass1");
    }
}
