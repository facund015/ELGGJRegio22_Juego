using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour {
    public float floatingSpeed;
    public float walkingSpeed;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 movementAir;
    private bool possessSwitch = false;

    private bool isInAirCurrent = false;

    public bool isArmored = false;
    public bool isHidden = false;
    public bool vesselInRange = false;
    GameObject vesselObj;
    GameObject currentVesselObject;
    bool intangible = false;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {

        // Intangibility 
        if (Input.GetKeyDown(KeyCode.T)) {
            intangible = !intangible;
            // Layer indexes 3 and 6 correspond to Player and PassableObject respectively
            Physics2D.IgnoreLayerCollision(3, 6, intangible);
        }
        //Recolecta los inputs vetical y horizontal del jugador cuando el switch de gravedad esta apagado
        if (!possessSwitch) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        //Recolecta solo el input horizontal cuando el switch de gravedad esta encendido
        else {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        //Activa la posesion de un objeto
        if (Input.GetKeyDown(KeyCode.G) && !possessSwitch && vesselInRange) {
            GravityOn();
            possessSwitch = true;
            currentVesselObject = vesselObj;
            movement.x = 0;
            movement.y = 0;
            if (vesselObj.CompareTag("Armor")) {
                isArmored = true;
                transform.position = vesselObj.transform.position;
                vesselObj.transform.parent.gameObject.SetActive(false);
            }
            else if (vesselObj.CompareTag("HidingSpot")) {
                isHidden = true;
                // Dissappear when hidden
                transform.position = vesselObj.transform.position;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }

        // Desactiva la posesion de objeto
        else if (Input.GetKeyDown(KeyCode.G) && possessSwitch && !isInAirCurrent) {
            movement.x = 0;
            movement.y = 0;
            GravityOff();
            if (currentVesselObject.CompareTag("Armor")) {
                isArmored = false;
                currentVesselObject.transform.parent.position = transform.position;
                currentVesselObject.transform.parent.gameObject.SetActive(true);
                currentVesselObject = null;
            }
            else {
                // Hiding spot
                isHidden = false;
                gameObject.GetComponent<Renderer>().enabled = true;
            }
            currentVesselObject = null;
        }
    }

    private void GravityOn() {
        rb.gravityScale = 50;
        possessSwitch = true;

    }

    private void GravityOff() {
        rb.gravityScale = 0;
        possessSwitch = false;
        isHidden = false;
        isArmored = false;
    }

    // Actualiza el motor de fisicas en un timer constante
    private void FixedUpdate() {

        if (!isInAirCurrent || isArmored) {
            if (!possessSwitch && !isHidden) {
                rb.MovePosition(rb.position + movement.normalized * floatingSpeed * Time.fixedDeltaTime);
                //Debug.Log(rb.position + movement.normalized * floatingSpeed * Time.fixedDeltaTime);

            }
            else if (!isHidden) {
                rb.MovePosition(rb.position + movement.normalized * walkingSpeed * Time.fixedDeltaTime);
            }
        }
        else {
            rb.MovePosition(rb.position + ((movementAir.normalized * 3f) + movement.normalized * 2f) * Time.fixedDeltaTime);
            //if (!possessSwitch && !isHidden)
            //{
            //    rb.MovePosition(rb.position + movement.normalized * floatingSpeed * Time.fixedDeltaTime);
            //}
            //else if (!isHidden)
            //{
            //    rb.MovePosition(rb.position + movement.normalized * walkingSpeed * Time.fixedDeltaTime);
            //}
        }
    }
    private void OnTriggerEnter2D( Collider2D collision ) {
        //Debug.Log("COLLISISODNAWDNAWD");
        if (collision.gameObject.CompareTag("Armor") || collision.gameObject.CompareTag("HidingSpot")) {
            vesselObj = collision.gameObject;
            vesselInRange = true;
        }
        if (collision.gameObject.CompareTag("Ghost")) {
            if (isArmored) {
                GravityOff();
            }
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if (collision.gameObject.CompareTag("Armor") || collision.gameObject.CompareTag("HidingSpot")) {
            vesselInRange = false;
            vesselObj = null;
        }
    }

    public void setAirCurrentDirection( Vector2 direction, bool status ) {
        isInAirCurrent = status;
        movementAir = direction;
        }
}