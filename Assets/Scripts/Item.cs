using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    KEY,
    COLOR,
    SWAP,
}

public class Item : MonoBehaviour
{
    [SerializeField]    ItemType itemType;
    [SerializeField]    ObjColor itemColor;

    private void Awake()
    {
        if (itemType == ItemType.COLOR || itemType == ItemType.SWAP)
        {
            SpriteRenderer sprite =  GetComponent<SpriteRenderer>();

            switch (itemColor)
            {
                case ObjColor.Blue:
                    SetColor(sprite, Color.blue);
                    break;

                case ObjColor.Red:
                    SetColor(sprite, Color.red);
                    break;

                case ObjColor.Yellow:
                    SetColor(sprite, Color.yellow);
                    break;
            }            
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            switch(itemType)
            {
                case ItemType.KEY:
                    GameMng.hasKey = true;
                    AudioManager.Instance.PlayEffect2(SoundClip.key);
                    break;

                case ItemType.COLOR:
                    {
                        PlayerController player = collision.GetComponent<PlayerController>();
                        player.Color = itemColor;
                    }
                    AudioManager.Instance.PlayEffect2(SoundClip.head_change);
                    break;

                case ItemType.SWAP:
                    {
                        StageController stage = FindObjectOfType<StageController>();
                        PlayerController player = collision.GetComponent<PlayerController>();

                        if (stage != null)
                            stage.Swap(player.Color, itemColor);
                    }
                    AudioManager.Instance.PlayEffect2(SoundClip.swap);
                    break;
            }
            gameObject.SetActive(false);
        }
    }

    void SetColor(SpriteRenderer sprite, float r, float g, float b)
    {
        SetColor(sprite, new Color(r, g, b));
    }

    void SetColor(SpriteRenderer sprite, Color color)
    {
        sprite.material.SetColor("_Color", color);
    }

    public void ResetItem()
    {
        gameObject.SetActive(true);
    }
}
