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
    public bool isPuzzle = false;
    private bool armorInLight = false;
    private int iFrames = 5;
    //private int layerMask = ~(1 << 2)  | ~(1 << 3) | ~(1 << 6) | ~(1 << 7) | ~(1 << 31) | ~(1 << 31);

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
            HitByLightP(isPuzzle);
        }

        if (inLight && Input.GetKeyDown(KeyCode.G))
        {
            cc.armor.armorInLight = true;
            cc.armor.lastHit = mousePos;
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
        
        inLight = false;
    }

    void HitByLightP(bool puzzleWindow)
    {
        if (cc.isArmored && cc.hasMirror && puzzleWindow)
        {
            RaycastHit2D hit = Physics2D.Raycast(shieldPos, mousePos, 1000f, 3);
            DrawLight(shieldPos, hit.point);

            Debug.DrawLine(shield.position, hit.point, Color.red);

            if (hit.transform.tag == "Crystal")
            {
                hit.transform.SendMessage("HitByLight");
            }
        }
        else if (cc.isArmored)
        {
            iFrames = 5;
            Debug.Log("Armored");
        }
        else
        {
            if (iFrames != 0)
            {
                iFrames--;
            } else
            {
                Death();
            }
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