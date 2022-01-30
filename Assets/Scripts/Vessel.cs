using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessel : MonoBehaviour
{
    public Transform shieldPos;
    public bool hasMirror;
    public GameObject mirror;
    public Vector2 lastHit;
    public bool armorInLight = false;
    public BoxCollider2D aoe;

    private Vector2 originPosition;

    void Start()
    {
        originPosition = transform.position;
        armorInLight = false;
    }

    private void Update()
    {
        if (armorInLight)
        {
            RaycastHit2D hit = Physics2D.Raycast(shieldPos.position, lastHit, 100f);
            if (hit.point != null)
            {
                float angle = Vector2.Angle(shieldPos.position, hit.point);
                Debug.Log(angle);
                float distance = Vector2.Distance(shieldPos.position, hit.point);
                aoe.offset = new Vector2(0, distance / 2f);
                aoe.size = new Vector2(aoe.size.x, distance);
                Debug.DrawLine(shieldPos.position, hit.point, Color.blue);
            }
        }
    }

    public void resetPosition()
    {
        transform.position = originPosition;
    }
}