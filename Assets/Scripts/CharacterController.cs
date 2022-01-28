using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float floatingSpeed;
    public float walkingSpeed;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool armorSwitch = false;
    ArmorEvents armorEvents;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        armorEvents = gameObject.GetComponent<ArmorEvents>();
    }

    void Update() {

        //Recolecta los inputs vetical y horizontal del jugador cuando el switch de gravedad esta apagado
        if (!armorSwitch && armorEvents.IsArmorInRange()) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            // Disable armor object
            armorEvents.GetArmorObject().SetActive(false);
        }
        //Recolecta solo el input horizontal cuando el switch de gravedad esta encendido
        else {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        //Activa la gravedad
        if (Input.GetKeyDown(KeyCode.G) && !armorSwitch) {
            rb.gravityScale = 50;
            armorSwitch = true;
        }
        //Desactiva la gravedad
        else if (Input.GetKeyDown(KeyCode.G) && armorSwitch) {
            rb.gravityScale = 0;
            armorSwitch = false;
            if (armorEvents.GetArmorObject() != null) {
                armorEvents.GetArmorObject().SetActive(true);
            }
        }
    }

    //Actualiza el motor de fisicas en un timer constante
    private void FixedUpdate() {
        if (!armorSwitch) {
            rb.MovePosition(rb.position + movement.normalized * floatingSpeed * Time.fixedDeltaTime);
        }
        else {
            rb.MovePosition(rb.position + movement.normalized * walkingSpeed * Time.fixedDeltaTime);
        }
    }
}