using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManage.Ins.RegisterEvent("Test1", Print);
        EventManage.Ins.ExecuteEvent("Test1");
    }
    private void Print()
    {
        Debug.Log("Test1");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
