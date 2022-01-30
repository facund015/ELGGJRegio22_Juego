using UnityEngine;

public class Aircurrent : MonoBehaviour {

    public Vector2 movement;

    GameObject player = null;

    // Start is called before the first frame update
    void Start() {
        movement.x = -1000.0f;
        movement.y = 0.0f;
    }

    private void OnTriggerEnter2D( Collider2D collision ) {
        if (collision.gameObject.CompareTag("Player")) {
            player = collision.gameObject;
            CharacterController playerController = player.GetComponent<CharacterController>();
            if (!playerController.isArmored) {
            playerController.setAirCurrentDirection(movement, true);
            player.GetComponent<Rigidbody2D>().AddForce(movement);

            }
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if (collision.gameObject.CompareTag("Player")) {
            CharacterController playerController = collision.gameObject.GetComponent<CharacterController>();
            playerController.setAirCurrentDirection(movement, false);
            player = null;
        }
    }
}
