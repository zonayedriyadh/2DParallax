using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Log
{
    private static readonly bool LOGGING_ENABLED = false;

    public static void L(object msg)
    {
        if(LOGGING_ENABLED)Log.L(msg);
    }

    public static void E(object msg)
    {
        if (LOGGING_ENABLED) Debug.LogError(msg);
    }

    public static void F(string format, params object[] args)
    {
        if (LOGGING_ENABLED) Debug.LogFormat(format, args);
    }
}
