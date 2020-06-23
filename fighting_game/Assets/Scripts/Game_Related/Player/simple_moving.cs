using UnityEngine;
using UnityEngine.UI;

/**
 * This is the class that is creating a simple moving direction for the players within the story scene
 */
public class simple_moving : MonoBehaviour
{
    // The rigidbody of the bullet/player
    public Rigidbody2D rb2d;
    public float speed = 1.5f;

    // If we are using the player then the animator has to be used in order to trigger the walk animation
    public Animator animator = null;

    // if we are moving to the right -- standard value
    public float movement_direction = 40f;

    // if we are within the story mode then this will be the button that starts the game
    public Button btn = null;

    // This indicates which player is being moved
    public bool isPlayerOne = true;

    // Start is called before the first frame update
    void Start()
    {
        // If an animator has been set (which means we are calling the script for player one in the trailer scene) then the 
        // walking hunter animation is being played and meanwhile the btn is being set to false (as initiation)
        if (isPlayerOne && animator!=null)
        {
            animator.SetInteger("Animation_Status", 1);
            btn.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // The hunter is running to the movement direction
        if (isPlayerOne)
            rb2d.velocity = new Vector2(movement_direction * speed, rb2d.velocity.y)*Time.deltaTime;
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // The player keeps walking to the right until he meets the other player
        // then the player stops and the button to start the game bevomes visible
        if (collision.gameObject.tag.Equals("Player") && animator != null)
        {
            speed = 0;
            animator.SetInteger("Animation_Status", 0);
            btn.gameObject.SetActive(true);
        }
    }
}
