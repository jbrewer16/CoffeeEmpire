using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public static class GlobalFunctions
{
    public static string FormatNumber(double value, bool asUSD = false)
    {
        string prefix = (asUSD) ? "$" : "";

        string[] suffixes = { "", "K", "Mill", "Bill", "Trill", "q", "Q", "s", "S", "t", "T", "u", "U" };
        double[] scales = { 1, 1e3, 1e6, 1e9, 1e12, 1e15, 1e18, 1e21, 1e24, 1e27, 1e30, 1e33, 1e36, 1e39, 1e42 };

        for (int i = suffixes.Length - 1; i >= 0; i--)
        {
            if (value >= scales[i])
            {
                double scaledValue = value / scales[i];
                if (value < 1000 && !asUSD)
                {
                    return prefix + scaledValue.ToString("0") + suffixes[i];
                }
                else
                {
                    return prefix + scaledValue.ToString("F2") + suffixes[i];
                }
                //return prefix + ((asUSD) ? scaledValue.ToString("F2") : scaledValue.ToString("0")) + suffixes[i];
            }
        }

        //return value.ToString("C2");
        return (asUSD) ? value.ToString("C2") : value.ToString("0");
    }
}
