using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public DoorHandler door;
    public bool engaged = false;
    private int pos;
    public Animator animator;

    void Start()
    {
        door.checks.Add(false);
        pos = door.checks.Count - 1;
    }

    void Update()
    {
        animator.SetBool("Active", engaged);
        door.checks[pos] = engaged;
    }

    void HitByLight()
    {
        engaged = true;
    }
}
