using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : BaseScene
{
    protected override void Register()
    {
        AddChannel(Channel.C1, Scene.Town);
    }
}
