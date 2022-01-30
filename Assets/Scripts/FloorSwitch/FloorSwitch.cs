using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour {
    public DoorHandler door;
    public bool engaged;
    private int pos;
    public Animator animator;
    public float radius = 0.1f;
    public Transform sensor;    
    void Start() {
        door.checks.Add(false);
        pos = door.checks.Count-1;
    }

    // Update is called once per frame
    void Update() {
        animator.SetBool("Active", engaged);
        door.checks[pos] = engaged;

        Collider2D[] colls = Physics2D.OverlapCircleAll(sensor.position, radius);
        engaged = false;
        for (int i=0; i<colls.Length; i++) {
            if (colls[i].gameObject.CompareTag("Player")) {
                engaged = colls[i].gameObject.GetComponent<CharacterController>().isArmored;
            }
        }
    }

    
}
