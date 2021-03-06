using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour {
    public float floatingSpeed;
    public float walkingSpeed;

    private Vector2 movement;
    private Vector2 movementAir;

    private bool isInAirCurrent = false;
    private bool isInCandleLight = false;

    public GameObject spirit;
    public GameObject knight;
    Animator spiritAnimator;
    Animator knightAnimator;

    private bool possessSwitch = false;
    public bool isArmored = false;
    public bool isHidden = false;
    public bool hasMirror = false;
    public bool vesselInRange = false;
    public bool mirrorInRange = false;
    public Vessel armor;
    //bool intangible = false;

    private Rigidbody2D rb;

    GameObject vesselObj;
    GameObject mirrorObject;
    GameObject currentVesselObject;
    GameObject currentMirrorObject;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(3, 10);
        Physics2D.IgnoreLayerCollision(3, 6, true);
        spiritAnimator = spirit.GetComponent<Animator>();
        knightAnimator = knight.GetComponent<Animator>();
        knight.SetActive(false);
    }


    void Update() {
        //SpawnToIdle();
        //Recolecta los inputs vetical y horizontal del jugador cuando el switch de gravedad esta apagado
        if (!possessSwitch) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        //Recolecta solo el input horizontal cuando el switch de gravedad esta encendido
        else {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        if (movement.normalized.x == -1) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (movement.normalized.x == 1) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //Activa la posesion de un objeto
        if (Input.GetKeyDown(KeyCode.Space) && !possessSwitch && vesselInRange) {
            GravityOn();
            possessSwitch = true;
            currentVesselObject = vesselObj;
            movement.x = 0;
            movement.y = 0;

            if (vesselObj.CompareTag("Mirror")) {
                hasMirror = true;
            }

            if (vesselObj.CompareTag("Armor")) {
                isArmored = true;
                ToggleForm(false);
                transform.position = vesselObj.transform.position;
                armor = vesselObj.GetComponentInParent<Vessel>();
                if (armor.hasMirror) {
                    currentMirrorObject = armor.mirror;
                    armor.mirror = null;
                    hasMirror = true;

                }
                vesselObj.transform.parent.gameObject.SetActive(false);
            }

            else if (vesselObj.CompareTag("HidingSpot")) {
                isHidden = true;
                currentVesselObject = vesselObj;
                vesselObj.transform.parent.GetComponent<HidingSpot>().ShowWill(true);
                // Dissappear when hidden
                spirit.SetActive(false);

            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && mirrorInRange && isArmored) {
            currentMirrorObject = mirrorObject;
            if (!hasMirror) {
                hasMirror = true;
                transform.position = mirrorObject.transform.position;
                mirrorObject.transform.parent.gameObject.SetActive(false);
            }
        }
        // Desactiva la posesion de objeto
        else if (Input.GetKeyDown(KeyCode.Space) && possessSwitch && !isInAirCurrent) {
            movement.x = 0;
            movement.y = 0;
            GravityOff();
            if (currentVesselObject != null) {
                if (currentVesselObject.CompareTag("Armor")) {
                    spirit.SetActive(true);
                    knight.SetActive(false);

                    // If player has shield then enable shield sprite on armor object 
                    GameObject rootVesselObj = currentVesselObject.transform.parent.gameObject;
                    rootVesselObj.GetComponent<Armor>().SetShieldSprite(hasMirror);
                    rootVesselObj.GetComponent<Armor>().transform.eulerAngles = transform.eulerAngles;
                    
                    currentVesselObject.transform.parent.position = transform.position;
                    currentVesselObject.transform.parent.gameObject.SetActive(true);

                    // Move Will a few units ahead of armor to prevent him from spawning at the wrong place
                    gameObject.transform.position = rootVesselObj.GetComponent<Armor>().exitPoint.position;
                    isArmored = false;

                    Vessel armor = currentVesselObject.GetComponentInParent<Vessel>();

                    if (hasMirror) {
                        armor.hasMirror = true;
                        armor.mirror = currentMirrorObject;
                        hasMirror = false;
                    }
                    currentVesselObject = null;
                    currentMirrorObject = null;
                }
                else {
                    // Hiding spot
                    isHidden = false;
                    spirit.SetActive(true);
                    vesselObj.transform.parent.GetComponent<HidingSpot>().ShowWill(false);
                }
                currentVesselObject = null;
                possessSwitch = false;
            }
        }


        if (isInCandleLight && !isArmored) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        spiritAnimator.SetFloat("speed", Mathf.Abs(movement.x));
        knightAnimator.SetFloat("speed", Mathf.Abs(movement.x));
        knightAnimator.SetBool("armored", isArmored);
        knightAnimator.SetBool("shield", hasMirror);
    }

    private void GravityOn() {
        rb.gravityScale = 25;
        // Layer indexes 3 and 6 correspond to Player and PassableObject respectively
        Physics2D.IgnoreLayerCollision(3, 6, false);
    }

    private void GravityOff() {
        rb.gravityScale = 0;
        isHidden = false;
        isArmored = false;
        // Layer indexes 3 and 6 correspond to Player and PassableObject respectively
        Physics2D.IgnoreLayerCollision(3, 6, true);
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
        }
    }
    private void OnTriggerEnter2D( Collider2D collision ) {
        if (collision.gameObject.CompareTag("Armor") || collision.gameObject.CompareTag("HidingSpot")) {
            vesselObj = collision.gameObject;
            vesselInRange = true;
        }

        if (collision.gameObject.CompareTag("Mirror")) {
            mirrorObject = collision.gameObject;
            mirrorInRange = true;
        }

        if (collision.gameObject.CompareTag("Ghost")) {
            if (isArmored) {
                ToggleForm(true);
                GravityOff();
                if (currentMirrorObject != null) {
                    currentMirrorObject.transform.parent.gameObject.SetActive(true);
                    Vessel mirror = currentMirrorObject.GetComponentInParent<Vessel>();
                    mirror.resetPosition();
                }
                currentVesselObject.transform.parent.gameObject.SetActive(true);
                armor = currentVesselObject.GetComponentInParent<Vessel>();
                armor.resetPosition();

                armor.hasMirror = false;
                currentVesselObject.transform.parent.GetComponent<Armor>().SetShieldSprite(false);

                armor.mirror = null;

                currentVesselObject = null;
                currentMirrorObject = null;

                hasMirror = false;
            }
        }

        if (collision.gameObject.CompareTag("Skeleton")) {
            isInCandleLight = true;
            Debug.Log("is in candle light");
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if (collision.gameObject.CompareTag("Armor") || collision.gameObject.CompareTag("HidingSpot")) {
            vesselInRange = false;
            vesselObj = null;
        }

        if (collision.gameObject.CompareTag("Mirror")) {
            mirrorInRange = false;
            mirrorObject = null;
        }

        if (collision.gameObject.CompareTag("Skeleton")) {
            isInCandleLight = false;
        }
    }

    public void setAirCurrentDirection( Vector2 direction, bool status ) {
        isInAirCurrent = status;
        movementAir = direction;
    }

    // True for spirit, false for ghost
    void ToggleForm( bool set ) {
        spirit.SetActive(set);
        knight.SetActive(!set);
    }
}