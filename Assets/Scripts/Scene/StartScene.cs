﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    float elapsedTime = 0f;
    bool executed = false;

    void Start()
    {
        SceneMng.Instance.AddScene<Title>(Scene.Title);
        SceneMng.Instance.AddScene<Town>(Scene.Town, true);
        SceneMng.Instance.AddScene<Game>(Scene.Game, true);
        //SceneMng.Instance.Enable(Scene.Title, false);

        AudioManager.Instance.LoadClip<Background>("AudioClip/Background/");
        AudioManager.Instance.LoadClip<SoundClip>("AudioClip/Effect/");

        GameMng.loadingUI = UIHelper.Instantiate<LoadingUI>("prefabs/LoadingUI", Vector3.zero, Quaternion.identity, null);
        GameMng.loadingUI.Init(false);

        DontDestroyOnLoad(GameMng.loadingUI.gameObject);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 0.0f && !executed)
        {
            executed = true;
            SceneMng.Instance.Enable(Scene.Title);
            AudioManager.Instance.PlayBackground(Background.Safety_Net, true, 0.66f);
        }
    }
}
