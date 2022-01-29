using UnityEngine;

public class Aircurrent : MonoBehaviour
{

    public Vector2 movement;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        movement.x = -1.0f;
        movement.y = 0.0f;
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (player != null)
    //    {
    //        CharacterController playerController = player.GetComponent<CharacterController>();
    //        if (!playerController.isArmored)
    //        {
    //            playerController.setAirCurrentDirection(movement, true);
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            CharacterController playerController = player.GetComponent<CharacterController>();
            playerController.setAirCurrentDirection(movement, true);
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController playerController = player.GetComponent<CharacterController>();
            playerController.setAirCurrentDirection(movement, false);
            player = null;
        }
    }
}
