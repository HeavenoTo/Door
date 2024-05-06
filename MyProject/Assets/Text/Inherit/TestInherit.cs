using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInherit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BaseTest test = new BaseTest();
        SonClass son = new SonClass();
        BaseTest baseTest = son;
        son.Test1();
        Debug.Log("----------------------");
        test.Test2();
        foreach (var item in EventManage.Ins.EventS)
        {
            Debug.Log(item.Key);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
