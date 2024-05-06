using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LeetcodeAlgorithm6 : MonoBehaviour
{
    void Start()
    {
        Convert("AB", 1);
    }

    public string Convert(string s, int numRows)
    {
        if (numRows < 2)
        {
            return s;
        }
        List<int> ints = new List<int>();
        bool del = false;
        int a = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (!del)
            {
                a++;
                ints.Add(a);
                Debug.Log($"+++++++{a}");
                if (a >= numRows)
                {
                    del = true;
                    continue;
                }
            }
            if (del)
            {
                a--;
                if (a <= 1)
                {
                    del = false;
                }
                Debug.Log($"--------{a}");
                ints.Add(a);
            }
        }
        string st = "";
        for (int i = 0; i <= numRows; i++)
        {
            for (int j = 0; j < ints.Count; j++)
            {
                if (i == ints[j])
                {
                    st += s[j];
                }
            }
        }
        Debug.Log($"{st}*************");
        return st;
    }
}

