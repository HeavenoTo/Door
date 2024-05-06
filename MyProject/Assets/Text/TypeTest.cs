using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class TypeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        unchecked //不检查
        {
            byte a = 255;
            byte b = 10;
            byte c = unchecked((byte)(a + b));
            byte d = unchecked((byte)(b - a - a));
            Debug.Log($"----{c}---{d}----");
        }


        dynamic ss = "st";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class TypeTest2
{

}
public class TypeTest3 : TypeTest2
{

}


internal class B
{

}
internal class D : B
{

}

public class TypeTest4
{
    public bool Contains()
    {
        return true;
    }
}