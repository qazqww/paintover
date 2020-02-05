using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Channel
{
    None,
    C1,
    C2,
}

public enum Scene
{
    None,
    Logo,
    Title,
    Town,
    Game
}

public class SceneMng : MonoBehaviour
{
    Dictionary<Scene, BaseScene> sceneDic = new Dictionary<Scene, BaseScene>();

    Scene curScene;
    object message;

    static SceneMng instance;
    public static SceneMng Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("Scene Manager", typeof(SceneMng));
                instance = obj.GetComponent<SceneMng>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public void Update()
    {
        Run();
    }

    public void Run()
    {
        if (sceneDic.ContainsKey(curScene))
        {
            object msg = sceneDic[curScene].Message;

            // 신으로부터 받은 메시지가 null일경우 함수를 종료
            if (msg == null)
                return;

            System.Type t = msg.GetType();

            if (t == typeof(Channel))
            {
                EventScene((Channel)msg);
                sceneDic[curScene].Message = null;
            }
        }
    }

    public T AddScene<T>(Scene scene, bool virtualLoad = false) where T : BaseScene
    {
        if (sceneDic.ContainsKey(scene))
            return sceneDic[scene].GetComponent<T>();

        GameObject obj = new GameObject(scene.ToString(), typeof(T));
        obj.transform.SetParent(transform);

        T t = obj.GetComponent<T>();
        t.Init();
        t.SetVirtualLoad(virtualLoad);

        sceneDic.Add(scene, t);
        return t;
    }

    public void Enable(Scene scene, bool loadScene = true)
    {
        // 등록되어 있는 신을 순회하면서 설정된 scene은 활성화, 설정되지 않은 Scene은 비활성화
        foreach (KeyValuePair<Scene, BaseScene> pair in sceneDic)
        {
            if (pair.Key == scene)
                pair.Value.gameObject.SetActive(true);
            else
                pair.Value.gameObject.SetActive(false);
        }

        // 설정된 값으로 현재 Scene값을 변경
        curScene = scene;

        if (curScene == Scene.Game)
            sceneDic[curScene].LoadAsyncScene();
        else
            sceneDic[curScene].LoadAsyncScene(scene.ToString());
    }

    public void EventScene(Channel c)
    {
        Scene changeScene = sceneDic[curScene].GetScene(c);
        if( changeScene != Scene.None )
            Enable(changeScene);
    }
}
