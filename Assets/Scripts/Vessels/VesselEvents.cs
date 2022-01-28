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

    private void OnTriggerEnter2D( Collider2D collision ) {
        vesselObj = collision.gameObject;
        inRange = ArmorOrHSpot(collision.gameObject);    
    }

    private void OnTriggerExit2D( Collider2D collision ) {
       inRange = ArmorOrHSpot(collision.gameObject);  
    }

    public bool InRange() {
        return inRange;
    }

    public GameObject GetVesselObj() {
        return vesselObj;
    }
    
    private bool ArmorOrHSpot(GameObject obj) {
        return obj.gameObject.CompareTag("Armor") || obj.gameObject.CompareTag("HidingSpot");
    }
}
