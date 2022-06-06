using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpot : MonoBehaviour
{

    public bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>() != null && !triggered)
            {
                triggered = true;
                transform.parent.GetComponent<ScoreController>().pointGet();
            }
        }
    }
}
