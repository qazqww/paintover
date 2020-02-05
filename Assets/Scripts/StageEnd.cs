using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnd : MonoBehaviour
{
    ResultUI resultUI;

    private void Awake()
    {
        resultUI = Instantiate(Resources.Load<ResultUI>("prefabs/ResultUI"));
        resultUI.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (GameMng.hasKey) // 게임이 클리어되는 경우 (플레이어가 키를 가지고 충돌 시)
            {
                if (collision.attachedRigidbody.velocity.y != 0) // 점프 중일 시 일단 대기
                    return;

                PlayerController player = collision.GetComponent<PlayerController>();                
                player.SetAniState(); // 대기 애니메이션
                GameMng.canInput = false; // 키 입력을 받을 수 없도록

                if (resultUI != null)
                    resultUI.Open();
            }
        }        
    }
}
