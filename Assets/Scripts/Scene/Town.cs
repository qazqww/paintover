using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Town : BaseScene
{
    protected override void Register()
    {
        AddChannel(Channel.C1, Scene.Game);
        AddChannel(Channel.C2, Scene.Title);
    } 
}
