using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm29 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Divide(-123,10));
    }

    public int Divide(int dividend, int divisor)
    {
        if (divisor == 1 || divisor == -1)
        {
            if (dividend == int.MinValue && divisor == -1)
                return int.MaxValue;
            return divisor == 1 ? dividend : -dividend;
        }

        // 确定符号为正号还是负号
        int s = dividend > 0 && divisor < 0 || dividend < 0 && divisor > 0 ? -1 : 1;

        dividend = dividend > 0 ? -dividend : dividend;
        divisor = divisor > 0 ? -divisor : divisor;

        int c = 0;
        while (dividend <= divisor)
        {
            ++c;
            dividend -= divisor;
        }

        return s == 1 ? c : -c;
    }
}
