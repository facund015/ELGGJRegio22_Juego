using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEvents : MonoBehaviour {
    public bool armorRange;
    public GameObject armorObj;
    // Start is called before the first frame update
    void Start() {
        armorRange = false;
    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter2D( Collider2D collision ) {
        armorObj = collision.gameObject;
        if (collision.gameObject.CompareTag("Armor")) {
            armorRange = true;
        }    
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if (collision.gameObject.CompareTag("Armor")) {
            armorRange = false;
        }  
    }

    public bool IsArmorInRange() {
        return armorRange;
    }

    public GameObject GetArmorObject() {
        return armorObj;
    }
}
