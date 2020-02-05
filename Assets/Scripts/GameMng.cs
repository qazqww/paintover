using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng 
{
    public static int lifeCount = 3;

    public static bool hasKey = false;

    // 현재 선택된 스테이지
    public static int selScene = 1;

    // 클리어한 스테이지 개수 (클리어한 가장 높은 스테이지)
    public static int clearCount = 0;

    // 클리어 정보(등급, 시간)를 담는 리스트
    public static List<ClearInfo> clearInfo = new List<ClearInfo>();

    public static bool canInput = true;

    public static float startTime = 0;
    public static float elapsedTime = 0;
    public static float ElapsedTime
    {
        get { return elapsedTime; }
        set { elapsedTime = value - startTime; }
    }

    public static ResultUI resultUI;
    public static LoadingUI loadingUI;
}
