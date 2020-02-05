using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapGrid : MonoBehaviour
{
    [SerializeField]    float width = 3;
    [SerializeField]    float height = 1.7f;
    [SerializeField]    int colCount = 7;    
    [SerializeField]    int elementCount = 42;
    [SerializeField]    GameObject prefab;

    List<Transform> blocks = new List<Transform>();   
    
    void Awake()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform t = transform.GetChild(i);
            DestroyImmediate(t.gameObject);
        }

        for (int i = 0; i < elementCount; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("prefabs/Tile_64_back"),
                  transform.position, Quaternion.identity) as GameObject;

            obj.transform.parent = transform;
            blocks.Add(obj.transform);
        }

        CalculatePos();
    }

    private void OnGUI()
    {
        CalculatePos();
    }

    void CalculatePos()
    {
        Vector3 startPos = transform.position;
        Vector3 nextPos = Vector3.zero;
        int colcount = 0;
        int rowCount = 0;
        for (int i = 0; i < blocks.Count; ++i)
        {
            nextPos.x = width * colcount;
            nextPos.y = height * rowCount;
            nextPos.z = startPos.z;

            blocks[i].position = startPos + nextPos;
            ++colcount;

            if (colcount == colCount)
            {
                ++rowCount;
                colcount = 0;
            }
        }
    }
}
