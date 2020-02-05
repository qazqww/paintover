using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    List<StageBtn> btnList = new List<StageBtn>();

    void Awake()
    {
        StageBtn[] btns = transform.GetComponentsInChildren<StageBtn>(true);
        btnList.AddRange(btns);

        for (int i=0; i<btnList.Count; i++)
        {
            // 버튼을 초기화하는 함수를 호출
            btnList[i].Init();
            // 클리어한 스테이지까지만 스테이지 버튼을 활성화
            if(i < GameMng.clearCount)
            {
                btnList[i].Clear = true;
                btnList[i].SetStageInfo(i+1, GameMng.clearInfo[i].clearScore);
            }
            else
            {
                btnList[i].Clear = false;
            }
        }

        btnList[GameMng.clearCount].DefaultStage(GameMng.clearCount + 1);
    }
}
