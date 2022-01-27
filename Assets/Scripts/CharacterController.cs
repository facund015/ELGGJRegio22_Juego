using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float floatingSpeed;
    public float walkingSpeed;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool gravitySwitch = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Recolecta los inputs vetical y horizontal del jugador cuando el switch de gravedad esta apagado
        if (!gravitySwitch)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        //Recolecta solo el input horizontal cuando el switch de gravedad esta encendido
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        //Activa la gravedad
        if (Input.GetKeyDown(KeyCode.G) && !gravitySwitch)
        {
            rb.gravityScale = 50;
            gravitySwitch = true;
        }
        //Desactiva la gravedad
        else if (Input.GetKeyDown(KeyCode.G) && gravitySwitch)
        {
            rb.gravityScale = 0;
            gravitySwitch = false;
        }
    }

    //Actualiza el motor de fisicas en un timer constante
    private void FixedUpdate()
    {
        if (!gravitySwitch)
        {
            rb.MovePosition(rb.position + movement.normalized * floatingSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movement.normalized * walkingSpeed * Time.fixedDeltaTime);
        }
    }
}
