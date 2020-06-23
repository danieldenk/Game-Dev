using UnityEngine;
/*
 * This is the responsible class for handling the player movement
 */
public class player_movement : MonoBehaviour
{
    // Player's speed, rigidbody and animator
    public float speed;
    public Rigidbody2D rb2d;
    public Animator animator;

    void FixedUpdate()
    {
        // This contains the direction we are moving to based on the input of our arrow/wasd keys
        float moveHorizontal = Input.GetAxisRaw("Horizontal") * speed;

        // after implementing the jump functionality, I concluded that it makes more sense to ground the player and remove the jump functionality again
        // therefore we still have this variable, but its not being used
        // I am leaving it here for the moment because I might implement some special attacks or attack combinations at a later point in the future
        float moveVertical = Input.GetAxis("Vertical");

        // If the player is standing still --> play idle
        if (moveHorizontal == 0)
            animator.SetInteger("Animation_Status", 0);
        // if the player is moving to the right ...
        else if (moveHorizontal > 0)
        {
            // ... play the walk animation
            animator.SetInteger("Animation_Status", 1);
            // ... set the flip of x to false (back to standard)
            GetComponent<SpriteRenderer>().flipX = false;
            // ... let the player move in the direction specified before
            rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        }
        // if the player is moving to the left ...
        else if (moveHorizontal < 0)
        {
            // set the walk animation 
            animator.SetInteger("Animation_Status", 1);
            // do a flip of the sprite so he faces the right direction, we want him to walk towards
            GetComponent<SpriteRenderer>().flipX = true;
            // we let the player move to the direction specified
            rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        }
    }
}