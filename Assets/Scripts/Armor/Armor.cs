using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject shieldSprite;

    void Start() {
        shieldSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetShieldSprite(bool set) {
        shieldSprite.SetActive(set);
    }
}
