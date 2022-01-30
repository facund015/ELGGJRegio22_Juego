using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject shieldSprite;
    public Transform exitPoint;
    void Start() {
        shieldSprite.SetActive(false);
    }

    public void SetShieldSprite(bool set) {
        shieldSprite.SetActive(set);
    }
}
