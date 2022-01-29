using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTarget : MonoBehaviour
{

    public SkeletonEnemyAI parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skeleton"))
        {
            parent.lastRefresh = Time.time;
        }
    }
}
