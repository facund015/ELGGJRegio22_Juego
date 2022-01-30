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
            RaycastHit2D hit = Physics2D.Raycast(shieldPos.position, lastHit, 100f, 3);
            if (hit.point != null)
            {
                Debug.DrawLine(shieldPos.position, hit.point, Color.blue);
            }

            if (hit.transform.tag == "Crystal")
            {
                hit.transform.SendMessage("HitByLight");
            }
        }
    }

    public void resetPosition()
    {
        transform.position = originPosition;
    }
}