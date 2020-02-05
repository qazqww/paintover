using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearInfo
{
    public int clearScore; // 별 1~3개
    public float playTime; // 경과 시간

    public ClearInfo(int score, float playTime)
    {
        clearScore = score;
        this.playTime = playTime;
    }
}
