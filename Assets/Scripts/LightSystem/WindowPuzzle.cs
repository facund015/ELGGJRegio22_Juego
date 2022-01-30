using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowPuzzle : MonoBehaviour
{
    public Transform lightPivot;
    public Vector2 boxSize;
    public float maxDistance;
    public BoxCollider2D aoe;

    void Start()
    {

    }

    void Update()
    {
        ShootLight();
    }

    void ShootLight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(lightPivot.position, boxSize, lightPivot.eulerAngles.z, lightPivot.up, maxDistance);

        if (hit.point != null)
        {
            float distance = Vector2.Distance(lightPivot.position, hit.point);
            aoe.offset = new Vector2(0, distance / 2f);
            aoe.size = new Vector2(aoe.size.x, distance);
            Debug.DrawLine(lightPivot.position, hit.point, Color.red);
        }
    }
}