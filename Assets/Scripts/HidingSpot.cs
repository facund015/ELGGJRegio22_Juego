using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour {
    public Sprite noWill;
    public Sprite will;
    public SpriteRenderer renderer;
    public bool willIn = false;
       // Start is called before the first frame update
    public void ShowWill(bool set) {
        renderer.sprite = set ? will : noWill;
    }
}
