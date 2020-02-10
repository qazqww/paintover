﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    Text time;
    int score;
    [SerializeField] GameObject[] stars = new GameObject[3];

    void Awake()
    {
        UIHelper.BindBtnFunc(transform, "Root/Center/Continue", OnClickContinue);
        UIHelper.BindBtnFunc(transform, "Root/Center/Restart", OnClickRestart);
        time = UtilHelper.Find<Text>(transform, "Root/Center/Time");
    }

    private void OnEnable()
    {
        score = 0;
        for (int i = 0; i < stars.Length; i++)
            stars[i].SetActive(false);

        if (GameMng.ElapsedTime < 15)
            score = 3;
        else if (GameMng.ElapsedTime < 30)
            score = 2;
        else
            score = 1;

        for (int i = 0; i < score; i++)
            stars[i].SetActive(true);

        GameMng.isResult = true;
    }

    private void OnDisable()
    {
        GameMng.isResult = false;
    }

    void OnClickContinue()
    {
        GameMng.canInput = true;
        SceneMng.Instance.EventScene(Channel.C1); 

        if (GameMng.selScene > GameMng.clearCount) // 새로 클리어한 판이면
        {
            ClearInfo clearInfo = new ClearInfo(score, GameMng.ElapsedTime);
            GameMng.clearCount = GameMng.selScene;
            GameMng.clearInfo.Add(clearInfo);
        }
        else if (GameMng.clearInfo[GameMng.selScene-1].clearTime > GameMng.ElapsedTime) // 기갱시
            GameMng.clearInfo[GameMng.selScene-1] = new ClearInfo(score, GameMng.ElapsedTime);

        gameObject.SetActive(false);
    }

    void OnClickRestart()
    {
        StageController stage = FindObjectOfType<StageController>();

        // 시간 초기화
        time.text = "0.00";
        GameMng.startTime = Time.time;

        if (stage != null)
            stage.Reset();
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        time.text = GameMng.ElapsedTime.ToString("F2");
    }
}
