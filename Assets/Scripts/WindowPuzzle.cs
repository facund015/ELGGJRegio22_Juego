using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WindowPuzzle : MonoBehaviour
{
    public Transform lightPivot;
    public Vector2 boxSize;

    private float innerAngle;

    void Start()
    {

    }

    void Update()
    {
        ShootLight();
    }

    void ShootLight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(lightPivot.position, boxSize, lightPivot.eulerAngles.z, lightPivot.up, 100f);

        if (hit.point != null)
        {
            Debug.DrawLine(lightPivot.position, hit.point, Color.red);
            if (hit.transform.tag == "Player")
            {
                hit.transform.SendMessage("HitByLight", true);
            }
        }
        else
        {
            Debug.DrawLine(lightPivot.position, -transform.up, Color.red);
        }
    }
}
