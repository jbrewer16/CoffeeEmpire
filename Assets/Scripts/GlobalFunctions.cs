using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public static class GlobalFunctions
{
    public static string FormatAsUSD(float value)
    {

        float mill = 1000000;
        float bill = 1000000000;
        float trill = 1000000000000;
        float quad = 1000000000000000;
        float quin = 1000000000000000000;

        if(value >= quin)
        {
            float quintillions = value / quin;
            return "$" + quintillions.ToString("F3") + "Q";
        } else if (value >= quad && value < quin)
        {
            float quadrillions = value / quad;
            return "$" + quadrillions.ToString("F3") + "q"; 
        } else if (value >= trill && value < quad)
        {
            float trillions = value / trill;
            return "$" + trillions.ToString("F3") + "Trill";
        } else if(value >= bill && value < trill)
        {
            float billions = value / bill;
            return "$" + billions.ToString("F3") + "Bill";
        } else if(value >= mill && value < bill)
        {
            float millions = value / mill;
            return "$" + millions.ToString("F3") + "Mill";
        } else
        {
            return value.ToString("C2");
        }

    }
}
