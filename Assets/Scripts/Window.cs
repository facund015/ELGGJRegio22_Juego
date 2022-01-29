using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Window : MonoBehaviour
{
    public Transform lightPivot;
    public Vector2 boxSize;
    public Light2D light;

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
        innerAngle = light.pointLightInnerAngle;

        RaycastHit2D hit = Physics2D.BoxCast(lightPivot.position, boxSize, lightPivot.eulerAngles.z, lightPivot.up, 100f);

        if (hit.point != null)
        {
            Debug.DrawLine(lightPivot.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(lightPivot.position, -transform.up, Color.red);
        }
    }
}
