using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour {
    public bool engaged;
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D( Collider other ) {
        if (other.CompareTag("Player")) {
            if (other.gameObject.GetComponent<CharacterController>().isArmored) {
                Debug.Log("Armor stays on!");
            }
        }
    }
}
