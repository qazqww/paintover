using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjColor
{
    Grey,
    Red,
    Blue,
    Yellow,
}

public class Block : MonoBehaviour
{
    [SerializeField]    ObjColor startColor = ObjColor.Red;
    [SerializeField]    bool greyBlock = false;
    [SerializeField]    bool startBlock = false;
    [SerializeField]    bool endBlock = false;
        
    Collider2D collision;
    List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    ObjColor color;
    public ObjColor Color
    {
        get { return color; }
        set
        {
            ObjColor objColor = value;
            
            if (!greyBlock)
                color = objColor;
            else
                objColor = ObjColor.Grey;

            switch (objColor)
            {
                case ObjColor.Yellow:
                    SetColor(0.3f, 0.3f, 0);
                    break;

                case ObjColor.Red:
                    SetColor(1, 0, 0);
                    break;

                case ObjColor.Blue:
                    SetColor(0, 0, 1);
                    break;

                case ObjColor.Grey:
                    SetColor(0.2f, 0.2f, 0.2f);
                    break;                    
            }
        }
    }

    void Awake()
    {
        sprites.AddRange(GetComponentsInChildren<SpriteRenderer>(true));

        if (!greyBlock)
            Color = startColor;
        else
            Color = ObjColor.Grey;

        // 오브젝트의 colliders 중에 trigger가 아닌거 찾아오기
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>(true);
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (!colliders[i].isTrigger)
            {
                collision = colliders[i];
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (collision != null)
            collision.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (collision != null)
            collision.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject != null && coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();

            // 스테이지를 클리어했을 경우
            if (endBlock && GameMng.hasKey)
                return;

            if (player != null && !greyBlock && player.Ready)
            {
                // 플레이어 색상과 발판 색상이 같을 때 -> 떨어짐
                if(player.Color == Color)
                {
                    Collider2D collider = GetComponent<Collider2D>();
                    if (collider != null)
                        collider.enabled = false;
                }
                // 다를 때 -> 발판 색 변경
                else
                {
                    if (rigidbody.velocity.y <= 0)
                    {
                        Color = player.Color;
                        AudioManager.Instance.PlayEffect(SoundClip.floor_change);
                    }
                }                
            }

            if (!player.Ready && startBlock)
                player.Ready = true;
        }        
    }

    void SetColor(float r, float g, float b)
    {
        for (int i = 0; i < sprites.Count; ++i)
        {
            sprites[i].material.SetFloat("_ColorR", r);
            sprites[i].material.SetFloat("_ColorG", g);
            sprites[i].material.SetFloat("_ColorB", b);
        }
    }

    public void ResetBlock()
    {
        Color = startColor;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = true;
    }
}
