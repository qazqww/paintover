using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    Text time;
    Text stageNum;
    Text hp;
    Button jumpBtn;
    Joystick joystick;
    StageController stage;

    public Vector2 Dir
    {
        get
        {
            if (joystick != null)
                return joystick.Dir;
            else return Vector2.zero;
        }
    }

    public void Awake()
    {
        stage = FindObjectOfType<StageController>();
        GameMng.startTime = Time.time;        
        time = UtilHelper.Find<Text>(transform, "TopRight/Time");
        stageNum = UtilHelper.Find<Text>(transform, "Top/Stage");
        hp = UtilHelper.Find<Text>(transform, "TopLeft/Image/Text");
        joystick = UtilHelper.Find<Joystick>(transform, "Joystick");
        jumpBtn = UtilHelper.Find<Button>(transform, "JumpBtn");

        stageNum.text = string.Format("Stage {0}", GameMng.selScene);
        hp.text = string.Format("x {0}", GameMng.lifeCount);

        UIHelper.BindBtnFunc(transform, "TopRight/ExitBtn", OnClickExit);
        UIHelper.BindBtnFunc(transform, "TopRight/RetryBtn", OnClickRetry);

        //joystick.Init();
    }
    
    void Update()
    {
        if (!GameMng.canInput) return;      

        GameMng.ElapsedTime = Time.time;
        time.text = GameMng.ElapsedTime.ToString("F2");
        hp.text = string.Format("x " +GameMng.lifeCount.ToString());
    }

    public void SetJumpEventFunc(UnityEngine.Events.UnityAction action)
    {
        jumpBtn.onClick.AddListener(action);
    }

    void OnClickExit()
    {
        SceneMng.Instance.EventScene(Channel.C1);
    }

    void OnClickRetry()
    {
        if (stage != null)
            stage.Reset();
    }
}
