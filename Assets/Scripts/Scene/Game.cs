using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : BaseScene
{
    protected override void Register()
    {
        AddChannel(Channel.C1, Scene.Town);
    }    

    public override void LoadAsyncScene()
    {
        LoadAsyncScene(GameMng.selScene.ToString());
    }
}
