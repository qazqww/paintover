using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    Text time;

    void Awake()
    {
        UIHelper.BindBtnFunc(transform, "Root/Center/Continue", OnClickContinue);
        UIHelper.BindBtnFunc(transform, "Root/Center/Restart", OnClickRestart);
        time = UtilHelper.Find<Text>(transform, "Root/Center/Time");
    }

    void OnClickContinue()
    {
        GameMng.canInput = true;
        SceneMng.Instance.EventScene(Channel.C1);

        int score = 0;

        if (GameMng.ElapsedTime < 15)
            score = 3;
        else if (GameMng.ElapsedTime < 30)
            score = 2;
        else
            score = 1;

        if (GameMng.selScene > GameMng.clearCount)
        {
            ClearInfo clearInfo = new ClearInfo(score, GameMng.ElapsedTime);
            GameMng.clearCount = GameMng.selScene;
            GameMng.clearInfo.Add(clearInfo);
        }

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
