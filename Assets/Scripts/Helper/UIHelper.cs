using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHelper 
{
    public static void BindBtnFunc(Transform t, string path, UnityEngine.Events.UnityAction action)
    {
        Button btn = UtilHelper.Find<Button>(t, path);
        if (btn != null)
            btn.onClick.AddListener(action);
    } 

    public static T Instantiate<T>(string path, Vector3 pos, Quaternion rot, Transform parent, bool active = true) where T : Component
    {
        T t = Resources.Load<T>(path);
        if (t != null)
        {
            t = GameObject.Instantiate(t, pos, rot, parent);
            t.gameObject.SetActive(active);
            return t;
        }
        return null;
    }
}
