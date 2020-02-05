using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    StageController stage;
    public StageController Stage
    {
        set { stage = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stage != null && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            stage.PlayerDeath();
    }
}
