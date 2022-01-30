using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public Transform lightPivot;
    public Vector2 boxSize;
    public float maxDistance;

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
            Debug.DrawLine(lightPivot.position, hit.point, Color.red);
            if (hit.transform.tag == "Player")
            {
                hit.transform.SendMessage("HitByLight", false);
            }
        }
    }
}