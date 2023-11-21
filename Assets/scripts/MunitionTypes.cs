using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MunitionTypes
{    
    public static Dictionary<shellType, float> shellDamage;

    public enum shellType
    {
        primary,
        secondary,
        special1,
        special2,
        special3
    }

    public enum shellState
    {
        active,
        slpash,
        explode
    }
}
