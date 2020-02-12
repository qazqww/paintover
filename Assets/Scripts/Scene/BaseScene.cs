using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BaseScene : MonoBehaviour
{
    Dictionary<Channel, Scene> channels = new Dictionary<Channel, Scene>();

    AsyncOperation asyncOperation;

    float elapsedTime = 0;
    bool load = false;
    bool virtualLoad = false;
    public void SetVirtualLoad(bool state)
    {
        virtualLoad = state;
    }

    object message;
    public object Message
    {
        get { return message; }
        set { message = value; }
    }

    protected void AddChannel(Channel ch, Scene sc)
    {
        if (!channels.ContainsKey(ch))
            channels.Add(ch, sc);
    }

    // 채널이 등록되어있는지 확인
    public bool ContainsKey(Channel ch)
    {
        if (channels.ContainsKey(ch))
            return true;
        return false;
    }

    public Scene GetScene(Channel ch)
    {
        if (channels.ContainsKey(ch))
            return channels[ch];
        return Scene.None;
    }

    public void Init()
    {
        Register();
    }

    private void Update()
    {
        Run();
    }

    protected virtual void Register()
    {

    }

    protected virtual void Run()
    {


    }

    public virtual void SceneUpdate(float delta)
    {

    }

    protected virtual IEnumerator IELoadScene(AsyncOperation operation)
    {
        while (!load)
        {
            // 신이 다 읽어 들일때까지 progress(상태값)를 받을 수 있도록
            SceneUpdate(operation.progress);

            load = operation.isDone;
            yield return null;
        }
    }

    protected virtual IEnumerator IELoadScene(AsyncOperation operation, bool none)
    {
        bool state = false;
        GameMng.loadingUI.SetActive(true);
        while (!state)
        {
            elapsedTime += Time.deltaTime;
            elapsedTime = Mathf.Clamp01(elapsedTime * 1.2f);
            GameMng.loadingUI.SetProgress(elapsedTime);

            if (elapsedTime >= 1.0f)
            {
                elapsedTime = 0;
                GameMng.loadingUI.SetActive(false);
                state = true;
            }

            yield return null;
        }
    }

    public virtual void LoadAsyncScene()
    {

    }

    // 현재 신의 상태 리턴
    public void LoadAsyncScene(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        if (virtualLoad)
            StartCoroutine(IELoadScene(asyncOperation, true));
        else
            StartCoroutine(IELoadScene(asyncOperation));
    }
}