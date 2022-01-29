using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intangibility : MonoBehaviour {

    public bool intangible;

    // Start is called before the first frame update
    void Start() {
        intangible = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            intangible = !intangible;
            // Layer indexes 3 and 6 correspond to Player and PassableObject respectively
            Physics2D.IgnoreLayerCollision(3, 6, intangible);
        }
    }

}
