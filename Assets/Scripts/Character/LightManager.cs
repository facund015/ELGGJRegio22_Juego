using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LightManager : MonoBehaviour
{
    public Camera cam;
    public Transform shield;
    public LineRenderer lineRender;

    private CharacterController cc;
    private Vector3 mousePos;
    private Vector2 shieldPos;
    private Vector2 lastHitPoint;
    public bool inLight = false;
    private bool isPuzzle = false;
    private bool armorInLight = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        shieldPos = shield.position;

        if (inLight)
        {
            HitByLight(isPuzzle);
        }

        if (inLight && Input.GetKeyDown(KeyCode.G))
        {
            cc.armor.armorInLight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Light Puzzle")
        {
            inLight = true;
            isPuzzle = true;
        }
        else if (collision.tag == "Light Area")
        {
            inLight = true;
            isPuzzle = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (cc.hasMirror)
        {
            lineRender.SetPosition(0, new Vector2(100, 0));
            lineRender.SetPosition(1, new Vector2(100, 0));
        }

        cc.armor.lastHit = lastHitPoint;
        inLight = false;
    }

    void HitByLight(bool puzzleWindow)
    {
        if (cc.isArmored && cc.hasMirror && puzzleWindow)
        {
            RaycastHit2D hit = Physics2D.Raycast(shieldPos, mousePos, 100f);
            DrawLight(shieldPos, hit.point);
            lastHitPoint = mousePos;

            //Debug.DrawRay(shield.position, mousePos, Color.blue);
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