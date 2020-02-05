using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumHelper
{
    public static T[] GetValues<T>()
    {
        T[] arrType = (T[])Enum.GetValues(typeof(T)); // T 형식의 Enum 속 값들을 T 배열 형태로 받아온다
        return arrType;
    }

    public static string[] EnumToString<T>(T t)
    {
        T[] arrType = (T[])Enum.GetValues(typeof(T));

        List<string> strList = new List<string>();

        for (int i = 0; i < arrType.Length; i++)
            strList.Add(arrType[i].ToString());

        return strList.ToArray();
    }

    public static T StringToEnum<T>(string str)
    {
        T t = (T)Enum.Parse(typeof(T), str);
        return t;
    }
}
