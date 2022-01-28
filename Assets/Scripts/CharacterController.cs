using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float floatingSpeed;
    public float walkingSpeed;
    public float speed;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool gravitySwitch = false;
    public bool hasArmor = false;
    private bool touchingEnemy = false;
    private int iFrames = 30;

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
        //if (hasArmor)
        //{
        //    rb.gravityScale = 50;
        //    gravitySwitch = true;
        //}
        ////Desactiva la gravedad
        //else if (!hasArmor)
        //{
        //    rb.gravityScale = 0;
        //    gravitySwitch = false;
        //}
    }

    //Actualiza el motor de fisicas en un timer constante
    private void FixedUpdate()
    {
        //if (!gravitySwitch)
        //{
        //    rb.MovePosition(rb.position + movement.normalized * floatingSpeed * Time.fixedDeltaTime);
        //}
        //else
        //{
        //    rb.MovePosition(rb.position + movement.normalized * walkingSpeed * Time.fixedDeltaTime);
        //}

        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        Debug.Log(movement);

        if (touchingEnemy)
        {
            iFrames--;
            if (iFrames == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Armor")
        {
            Destroy(collision.gameObject);
            //rb.AddForce(Vector3.up * 100000000);
            gravitySwitch = true;
            rb.gravityScale = 50;
            hasArmor = true;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            touchingEnemy = true;
            if (hasArmor)
            {
                hasArmor = false;
                gravitySwitch = false;
                rb.gravityScale = 0;
            } else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            touchingEnemy = false;
            iFrames = 30;
        }
    }
}
