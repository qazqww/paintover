using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]    ObjColor startColor = ObjColor.Red;
    ObjColor color;
    Collider2D collision;
    List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    public ObjColor Color
    {
        get { return color; }
        set
        {
            color = value;

            switch (color)
            {
                case ObjColor.Yellow:
                    SetColor(0.9f, 0.9f, 0);
                    break;

                case ObjColor.Red:
                    SetColor(1, 0, 0);
                    break;

                case ObjColor.Blue:
                    SetColor(0, 0, 1);
                    break;
            }
        }
    }
    
    private void Awake()
    {
        sprites.AddRange(GetComponentsInChildren<SpriteRenderer>(true));        
        Color = startColor;
        //Collider2D[] colliders = GetComponentsInChildren<Collider2D>(true);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        PlayerController player = coll.GetComponent<PlayerController>();
        if(player != null)
        {
            if(player.Color == Color)
                gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (collision != null)
            collision.enabled = true;
    }

    void SetColor(float r, float g, float b)
    {
        for (int i = 0; i < sprites.Count; ++i)
        {
            sprites[i].color = new Vector4(r, g, b, 1);
            GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }
    }

    public void ResetLaser()
    {
        Color = startColor;
        gameObject.SetActive(true);
    }
}