using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LightManager : MonoBehaviour
{
    public Camera cam;
    public Transform shield;
    private CharacterController cc;
    private Vector3 mousePos;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    void HitByLight(bool puzzleWindow)
    {
        if (cc.isArmored && puzzleWindow)
        {
            RaycastHit2D hit = Physics2D.Raycast(shield.position, mousePos, 10f);
            //Debug.Log(hit.point);
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

    void Death()
    {
        Debug.Log("Death");
    }
}