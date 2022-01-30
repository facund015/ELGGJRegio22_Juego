using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour {
    public DoorHandler door;
    public bool engaged;
    private int pos;
    public Animator animator;
    public float radius = 0.1f;
    public Transform sensor;    
    public AudioSource sound;
    public AudioClip buttonOn;
    public AudioClip buttonOff;
    void Start() {
        door.checks.Add(false);
        pos = door.checks.Count-1;
        sound.clip = buttonOn;
        sound.Stop();
    }

    // Update is called once per frame
    void Update() {
        animator.SetBool("Active", engaged);
        door.checks[pos] = engaged;

        Collider2D[] colls = Physics2D.OverlapCircleAll(sensor.position, radius);
        engaged = false;
        for (int i=0; i<colls.Length; i++) {

            if (colls[i].gameObject.CompareTag("Player")) {
                engaged = colls[i].gameObject.GetComponent<CharacterController>().isArmored;
                if (engaged) {
                    sound.Play();
                    return;
                }
            }
            else if (colls[i].gameObject.CompareTag("Armor")) {
                engaged = true;
                sound.clip = buttonOff;
                sound.Play();
                return;

            }
        }
    }

    
}
