using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour {
    public DoorHandler door;
    public bool engaged;
    private int pos;
    public Animator animator;
    void Start() {
        door.checks.Add(false);
        pos = door.checks.Count-1;
    }

    // Update is called once per frame
    void Update() {
        animator.SetBool("Active", engaged);
        door.checks[pos] = engaged;
    }

    private void OnTriggerEnter2D( Collider2D other ) {
        if (other.CompareTag("Player")) {
            engaged = other.GetComponent<CharacterController>().isArmored;
            animator.SetBool("Active", true);
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if (collision.CompareTag("Player")) {
            engaged = !collision.GetComponent<CharacterController>().isArmored;
            animator.SetBool("Active", engaged);
        }
    }
}
