using System;
using System.Collections;
using System.Collections.Generic;
public class EventManage
{
    public static EventManage Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = new EventManage();
            }
            return _ins;
        }
    }
    private static EventManage _ins;

    public Dictionary<string, Action> EventS;

    public void RegisterEvent(string key, Action call)
    {
        if (EventS == null) EventS = new Dictionary<string, Action>();
        EventS[key] = call;
    }
    public void ExecuteEvent(string key)
    {
        if (EventS.ContainsKey(key)) EventS[key]();
    }
    public void DelEvent(string key)
    {
        if (EventS.ContainsKey(key)) EventS.Remove(key);
    }
}
