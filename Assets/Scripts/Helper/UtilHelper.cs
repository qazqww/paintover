using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilHelper 
{
    // path 경로의 게임오브젝트에서 T 컴포넌트를 얻어오는 코드 (및 오브젝트 활성화)
    public static T Find<T>(Transform t, string path, bool active = true) where T : Component
    {
        Transform obj = t.Find(path);

        if (obj != null)
        {
            obj.gameObject.SetActive(active);
            return obj.GetComponent<T>();
        }

        return null;
    }

}
