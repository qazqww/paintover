using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    float elapsedTime = 0f;

    void Start()
    {
        SceneMng.Instance.AddScene<Title>(Scene.Title);
        SceneMng.Instance.AddScene<Town>(Scene.Town, true);
        SceneMng.Instance.AddScene<Game>(Scene.Game, true);
        //SceneMng.Instance.Enable(Scene.Title, false);

        AudioManager.Instance.LoadClip<BackgroundType>("AudioClip/Background/");
        AudioManager.Instance.LoadClip<SoundType>("AudioClip/Effect/");

        GameMng.loadingUI = UIHelper.Instantiate<LoadingUI>("prefabs/LoadingUI", Vector3.zero, Quaternion.identity, null);
        GameMng.loadingUI.Init(false);

        DontDestroyOnLoad(GameMng.loadingUI.gameObject);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 1.0f)
        {
            SceneMng.Instance.Enable(Scene.Title);
            AudioManager.Instance.PlayBackground(BackgroundType.casual_04_loop.ToString(), true, 0.66f);
        }
    }
}
