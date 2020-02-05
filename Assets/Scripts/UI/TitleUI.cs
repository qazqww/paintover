using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    private void OnClickPlay()
    {
        AudioManager.Instance.PlayUISound(SoundType.btn_click.ToString());

        SceneMng.Instance.EventScene(Channel.C1);

        GameMng.loadingUI.SetProgress(0);
        GameMng.loadingUI.SetActive(true);
    }

    private void OnClickQuit()
    {
        AudioManager.Instance.PlayUISound(SoundType.btn_click.ToString());
        Application.Quit();
    }
    
    void Awake()
    {
        UIHelper.BindBtnFunc(transform, "PlayBtn", OnClickPlay);
        UIHelper.BindBtnFunc(transform, "QuitBtn", OnClickQuit);
    }
}
