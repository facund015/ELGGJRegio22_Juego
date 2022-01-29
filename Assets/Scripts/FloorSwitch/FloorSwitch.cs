using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour {
    public GameObject groundCheck;
    public float sensorRadius = 0.3f;
    public LayerMask targetLayer;
    public bool engaged;
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        Collider2D colliders = Physics2D.OverlapCircle(groundCheck.transform.position, sensorRadius, targetLayer);
        engaged = colliders != null;

        if (engaged) {
            // Do something about the animations
            Debug.Log("Floor switch engaged!");
        } else {
            // Do not do something about the animations
        }
    }
}
