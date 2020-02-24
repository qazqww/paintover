using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    void OnClickPlay()
    {
        //AudioManager.Instance.PlayUISound(SoundType.click.ToString());

        SceneMng.Instance.EventScene(Channel.C1);

        GameMng.loadingUI.SetProgress(0);
        GameMng.loadingUI.SetActive(true);
    }

    void OnClickQuit()
    {
        AudioManager.Instance.PlayEffect(SoundClip.click. ToString());
        Application.Quit();
    }
    
    void Awake()
    {
        UIHelper.BindBtnFunc(transform, "PlayBtn", OnClickPlay);
        UIHelper.BindBtnFunc(transform, "QuitBtn", OnClickQuit);
    }
}
