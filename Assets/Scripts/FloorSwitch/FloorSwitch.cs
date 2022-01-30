using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour {
    public bool engaged;
    public Animator animator;
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        animator.SetBool("Active", engaged);
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
