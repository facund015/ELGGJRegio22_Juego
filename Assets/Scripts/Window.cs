using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public Transform lightFirePoint;
    public Transform lightTransform;
    public BoxCollider2D col;
    public LineRenderer lineRenderer;

    void Start()
    {
        lightTransform = GetComponent<Transform>();
    }

    void Update()
    {
        ShootLight();
    }

    void ShootLight()
    {
        if (Physics2D.Raycast(lightTransform.position, -transform.up))
        {
            RaycastHit2D hit = Physics2D.Raycast(lightTransform.position, -transform.up * 100);
            Draw2DRay(lightFirePoint.position, hit.point);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);

        float lineLenght = Vector2.Distance(startPos, endPos);
        col.size = new Vector2(1f, lineLenght);
        Vector2 midPoint = (startPos + endPos) / 2f;
        col.transform.position = midPoint;
    }
}
