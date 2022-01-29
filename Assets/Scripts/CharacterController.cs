using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour {
    public float floatingSpeed;
    public float walkingSpeed;

    private int iFrames = 60;
    private bool touchingEnemy = false;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool possessSwitch = false;
    VesselEvents vesselEvents;
    bool enableMovement = true;
    public bool isArmored = false;
    public bool isHidden = false;
    bool vesselInRange = false;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        vesselEvents = gameObject.GetComponent<VesselEvents>();
    }

    void Update() {

        //Recolecta los inputs vetical y horizontal del jugador cuando el switch de gravedad esta apagado
        if (!possessSwitch) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        //Recolecta solo el input horizontal cuando el switch de gravedad esta encendido
        else {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        //Activa la gravedad
        if (Input.GetKeyDown(KeyCode.G) && !possessSwitch && vesselEvents.InRange()) {
            TurnOnGravity();
        }
        //Desactiva la gravedad
        else if (Input.GetKeyDown(KeyCode.G) && possessSwitch) {
            TurnOffGravity();
        }
    }

    private void TurnOnGravity()
    {
        rb.gravityScale = 50;
        possessSwitch = true;
        if (vesselEvents.GetVesselObj().CompareTag("Armor"))
        {
            isArmored = true;
            enableMovement = true;
        }
        else if (vesselEvents.GetVesselObj().CompareTag("HidingSpot"))
        {
            isHidden = true;
            enableMovement = false;
            // Move player to hiding spot, since it's not suppossed to move
            gameObject.transform.position = vesselEvents.GetVesselObj().transform.position;
        }
        vesselEvents.GetVesselObj().transform.parent.gameObject.SetActive(false);
    }

    private void TurnOffGravity()
    {
        rb.gravityScale = 0;
        possessSwitch = false;
        if (vesselEvents.GetVesselObj() != null)
        {
            vesselEvents.GetVesselObj().transform.parent.position = gameObject.transform.position;
            vesselEvents.GetVesselObj().transform.parent.gameObject.SetActive(true);

        }
        enableMovement = true;
        isHidden = false;
        isArmored = false;
    }

    //Actualiza el motor de fisicas en un timer constante
    private void FixedUpdate() {
        if (!possessSwitch && enableMovement) {
            rb.MovePosition(rb.position + movement.normalized * floatingSpeed * Time.fixedDeltaTime);
        }
        else if (enableMovement) {
            rb.MovePosition(rb.position + movement.normalized * walkingSpeed * Time.fixedDeltaTime);
        }

        if (touchingEnemy)
        {
            iFrames--;
            if (iFrames == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            
            if (isArmored)
            {
                TurnOffGravity();
                touchingEnemy = true;
            } else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            touchingEnemy = false;
            iFrames = 60;
        }
    }

}