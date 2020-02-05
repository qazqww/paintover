using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    Image background;
    Image btnImage;
    Vector3 startPosition;
    Vector2 dir;
    public Vector2 Dir
    {
        get { return dir; }
    }

    void Awake()
    {
        background = GetComponent<Image>();
        btnImage = UtilHelper.Find<Image>(transform, "Image");
        startPosition = btnImage.transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(eventData);
        dir = Vector2.zero;
        btnImage.transform.position = startPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        // 위치값을 되돌려주는 함수 (마우스 위치) => 위치값이 Rect 영역 안에 있다면 true 리턴
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle
            (background.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            Vector2 sizeDelta = background.rectTransform.sizeDelta;

            btnImage.transform.localPosition = pos;
            //pos.x = Mathf.Clamp(pos.x, -(sizeDelta.x / 2), sizeDelta.x / 2);
            //pos.y = Mathf.Clamp(pos.y, -(sizeDelta.y / 2), sizeDelta.y / 2);

            if (pos.magnitude > 75)
            {
                pos.Normalize();
                btnImage.transform.localPosition = pos * 75;
            }

            dir = pos.normalized;
        }        
    }
}
