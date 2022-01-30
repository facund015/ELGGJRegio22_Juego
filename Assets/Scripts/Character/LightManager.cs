using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LightManager : MonoBehaviour
{
    public Camera cam;
    public Transform shield;
    public LineRenderer lineRender;
    public float maxDistance;

    private CharacterController cc;
    private Vector3 mousePos;
    private Vector2 shieldPos;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        shieldPos = shield.position;
    }

    void HitByLight(bool puzzleWindow)
    {
        if (cc.isArmored && puzzleWindow)
        {
            RaycastHit2D hit = Physics2D.Raycast(shieldPos, mousePos, 100f);

            DrawLight(shieldPos, hit.point);

            Debug.DrawRay(shield.position, mousePos, Color.blue);
            Debug.DrawLine(shield.position, hit.point, Color.red);
        }
        else if (cc.isArmored)
        {
            Debug.Log("Armored");
        }
        else
        {
            Death();
        }
    }

    void DrawLight(Vector2 startPos, Vector2 endPos)
    {
        lineRender.SetPosition(0, startPos);
        lineRender.SetPosition(1, endPos);
    }

    void Death()
    {
        Debug.Log("Death");
    }
}