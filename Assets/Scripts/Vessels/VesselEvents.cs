using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselEvents : MonoBehaviour {
    public bool inRange;
    public GameObject vesselObj;
    // Start is called before the first frame update
    void Start() {
        inRange = false;
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter2D( Collider2D collision ) {
        vesselObj = collision.gameObject;
        if (collision.gameObject.CompareTag("Vessel")) {
            inRange = true;
        }    
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if (collision.gameObject.CompareTag("Vessel")) {
            inRange = false;
        }  
    }

    public bool InRange() {
        return inRange;
    }

    public GameObject GetVesselObj() {
        return vesselObj;
    }

    public bool GetMovementFlag() {
        return vesselObj.GetComponentInParent<MovementFlag>().movementAllowed;
    }
}
