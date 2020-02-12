using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    Transform btnOpen;
    Transform btnLock;
    List<Image> star = new List<Image>();
    Text stageText;
    Image check;
    Button button;
    int stageIdx;

    bool clear = false;
    public bool Clear
    {
        get { return clear; }
        set
        {
            if (button != null) button.enabled = value;
            clear = value;
            if (clear)
                btnOpen.gameObject.SetActive(true);
            else
                btnLock.gameObject.SetActive(true);
        }
    }

    public void Init()
    {
        btnOpen = UtilHelper.Find<Transform>(transform, "Open", false);
        btnLock = UtilHelper.Find<Transform>(transform, "Lock", false);

        if (btnOpen != null)
        {
            stageText = UtilHelper.Find<Text>(btnOpen, "Text");
            star.Add(UtilHelper.Find<Image>(btnOpen, "StarGroup/1/Image"));
            star.Add(UtilHelper.Find<Image>(btnOpen, "StarGroup/2/Image"));
            star.Add(UtilHelper.Find<Image>(btnOpen, "StarGroup/3/Image"));
            check = UtilHelper.Find<Image>(btnOpen, "Check");
        }
        button = UtilHelper.Find<Button>(transform, "forward");
        UIHelper.BindBtnFunc(transform, "forward", OnClickBtn);
    }    

    void OnClickBtn()
    {
        GameMng.selScene = stageIdx;
        SceneMng.Instance.EventScene(Channel.C1);
    }

    public void SetStageInfo(int stage, int score)
    {
        stageIdx = stage;
        if (stageText != null) stageText.text = stage.ToString();

        // 게임을 반복해서 플레이할 경우 스테이지 클리어 정보가 달라질 수 있기 때문에
        for (int i = 0; i < star.Count; i++)
            star[i].gameObject.SetActive(false);
        for (int i = 0; i < score; i++)
            star[i].gameObject.SetActive(true);
        check.gameObject.SetActive(true);
    }

    public void DefaultStage(int stageIdx)
    {
        this.stageIdx = stageIdx;
        stageText.text = stageIdx.ToString();

        btnOpen.gameObject.SetActive(true);

        Transform star = btnOpen.transform.Find("StarGroup");
        if (star != null)
            star.gameObject.SetActive(false);

        Transform check = btnOpen.transform.Find("Check");
        if (check != null)
            check.gameObject.SetActive(false);

        btnLock.gameObject.SetActive(false);
        button.enabled = true;
    }
}
