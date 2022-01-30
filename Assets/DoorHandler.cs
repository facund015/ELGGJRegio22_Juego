using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoorHandler : MonoBehaviour
{

    Animator animator;

    public List<bool> checks = new List<bool>();
    public bool opened;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(checks[0]);
        if (checks.All(x => x))
        {
            opened = true;
            animator.SetBool("Opened", true);
        } else
        {
            opened = false;
            animator.SetBool("Opened", false);
        }
    }
}
