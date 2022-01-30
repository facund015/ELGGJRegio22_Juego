using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour {
    public Sprite noWill;
    public Sprite will;
    public SpriteRenderer sRenderer;
    public AudioSource sound;
    public AudioClip enterSound, exitSound;

    public bool willIn = false;
       // Start is called before the first frame update
    public void ShowWill(bool set) {
        sRenderer.sprite = set ? will : noWill;

    }
}
