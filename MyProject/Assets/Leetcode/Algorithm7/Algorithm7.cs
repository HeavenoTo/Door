using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm7 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Reverse(-123));
    }

    public int Reverse(int x)
    {
        // bool owe = false;
        // string s = x.ToString();
        // if (s[0] == '-')
        // {
        //     s = s.Substring(1);
        //     owe = true;
        // }
        // string a = "";
        // for (int i = s.Length - 1; i >= 0; i--)
        // {
        //     a += s[i];
        // }
        // int.TryParse(a, out int b);
        // if (owe)
        // {
        //     b = -b;
        // }
        // return b;

        long y = x % 10; 
        while (x / 10 > 0 || x / 10 < 0)
        {
            x = x / 10;
            y = y * 10 + x % 10;
        }
        if (y > int.MaxValue || y < int.MinValue)
        {
            return 0;
        }
        else
        {
            return (int)y;
        }


    }
}
