using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 playerDir;

    PlayerController player;
    InGameUI inGameUI;

    [SerializeField]
    ObjColor playerColor = ObjColor.Red;

    List<Item> itemList = new List<Item>();
    List<Block> blockList = new List<Block>();
    List<Laser> laserList = new List<Laser>();
   
    void Awake()
    { 
        GameMng.hasKey = false;

        Transform startObj = UtilHelper.Find<Transform>(transform, "Stage/root/StartTrans");
        Transform endObj = UtilHelper.Find<Transform>(transform, "Stage/StageEnd");

        playerDir = endObj.position - startObj.position;

        blockList.AddRange(GetComponentsInChildren<Block>(true));
        itemList.AddRange(GetComponentsInChildren<Item>(true));
        laserList.AddRange(GetComponentsInChildren<Laser>(true));

        GameMng.resultUI = Instantiate(Resources.Load<ResultUI>("prefabs/ResultUI"));
        GameMng.resultUI.gameObject.SetActive(false);

        inGameUI = FindObjectOfType<InGameUI>();
        if(inGameUI != null)
            inGameUI.SetJumpEventFunc(OnClickJump); // 무엇

        if (startObj != null)
        {
            startPosition = startObj.position;

            GameObject waterObj = GameObject.Find("Water");
            WaterTrigger waterTrg = waterObj.GetComponent<WaterTrigger>();

            if (waterTrg != null)
                waterTrg.Stage = this;
        }

        player = Resources.Load<PlayerController>("prefabs/sword_man");
        player = Instantiate(player, startPosition, Quaternion.identity);
        player.Color = playerColor;
    }
    
    void Update()
    {
        if (!GameMng.canInput)
            return;

        else if (inGameUI != null)
            player.KeyInput(inGameUI.Dir);
    }

    // Swap 아이템
    public void Swap(ObjColor source, ObjColor target)
    {
        List<Block> sList = new List<Block>();
        List<Block> tList = new List<Block>();

        for (int i = 0; i < blockList.Count; ++i)
        {
            if (blockList[i].Color == source)
                sList.Add(blockList[i]);

            else if (blockList[i].Color == target)
                tList.Add(blockList[i]);
        }

        for (int i = 0; i < sList.Count; ++i)
            sList[i].Color = target;

        for (int i = 0; i < tList.Count; ++i)
            tList[i].Color = source;
    }

    public void Reset()
    {
        GameMng.lifeCount = 3;
        GameMng.canInput = true;
        GameMng.hasKey = false;

        player.ResetPlayer(startPosition, playerDir);
        player.Color = playerColor;

        ResetItems();
        Resetblock();
        ResetLaser();
    }

    public void ResetItems()
    {
        for (int i = 0; i < itemList.Count; ++i)
            itemList[i].ResetItem();
    }

    public void Resetblock()
    {
        for (int i = 0; i < blockList.Count; ++i)
            blockList[i].ResetBlock();
    }

    public void ResetLaser()
    {
        for (int i = 0; i < laserList.Count; i++)
            laserList[i].ResetLaser();
    }

    public void PlayerDeath()
    {
        if (GameMng.lifeCount > 0)
        {
            GameMng.hasKey = false;
            GameMng.lifeCount--;
            
            player.ResetPlayer(startPosition, playerDir);
            player.Color = playerColor;

            ResetItems();
            Resetblock();
            ResetLaser();
        }
            
        else
            Destroy(player.gameObject);
    }

    void OnClickJump()
    {
        if (!GameMng.canInput)
            return;
        player.Jump(inGameUI.Dir.y);
    }
}
