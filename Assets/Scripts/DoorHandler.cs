using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoorHandler : MonoBehaviour
{

    Animator animator;

    public bool Porticullis = false;
    public List<bool> checks = new List<bool>();
    public bool opened;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (checks.All(x => x))
        {
            opened = true;
            animator.SetBool("Opened", true);
        } else
        {
            opened = false;
            animator.SetBool("Opened", false);
        }

        if (Porticullis)
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (opened)
            {
                collider.isTrigger = true;
            }
            else
            {
                collider.isTrigger = false;
            }
        }
    }
}
