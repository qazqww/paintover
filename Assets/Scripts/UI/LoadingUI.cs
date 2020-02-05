using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = UtilHelper.Find<Slider>(transform, "Slider");
        Init(false);
    }

    public void Init(bool state)
    {
        SetActive(state);
    }

    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }

    public void SetProgress(float delta)
    {
        slider.value = delta;
    }
}
