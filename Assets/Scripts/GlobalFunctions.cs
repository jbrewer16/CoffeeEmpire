using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public static class GlobalFunctions
{
    public static string FormatNumber(float value, bool asUSD = false)
    {

        float k = 1000;
        float mill = 1000000;
        float bill = 1000000000;
        float trill = 1000000000000;
        float quad = 1000000000000000;
        float quin = 1000000000000000000;

        string prefix = (asUSD) ? "$" : "";
        string formatString = "F2";

        if(value >= quin)
        {
            float quintillions = value / quin;
            return prefix + quintillions.ToString(formatString) + "Q";
        } else if (value >= quad && value < quin)
        {
            float quadrillions = value / quad;
            return prefix + quadrillions.ToString(formatString) + "q"; 
        } else if (value >= trill && value < quad)
        {
            float trillions = value / trill;
            return prefix + trillions.ToString(formatString) + "Trill";
        } else if(value >= bill && value < trill)
        {
            float billions = value / bill;
            return prefix + billions.ToString(formatString) + "Bill";
        } else if(value >= mill && value < bill)
        {
            float millions = value / mill;
            return prefix + millions.ToString(formatString) + "Mill";
        } else if(value >= k && value < mill)
        {
            float thous = value / k;
            return prefix + thous.ToString(formatString) + "K";
        } else
        {
            return (asUSD) ? value.ToString("C2") : value.ToString();
        }

    }
}
