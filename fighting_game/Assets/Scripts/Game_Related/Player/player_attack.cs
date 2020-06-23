using UnityEngine;

/**
 * This is the class to manage player related behaviour  
 */
public class player_attack : MonoBehaviour
{
    // Player's animator
    public Animator animator;

    // This is the bullet prefab
    public GameObject shot;

    // This is an array of floats containing the Hitrate and Nexthit of two main attacks
    // based on that we can achieve a cooldown of the attacks
    public float[] hit_and_next_rates;

    // This audioclip is containing the sound of the player attack
    public AudioClip sound_effect;

    void Start()
    {
        // Initiation phase
        animator = GetComponent<Animator>();
    }
    void Update()
    {       
        // If Key 1 is pressed then an attack is being performed (play animation: attack_b) 
        // Commented it out because its too much with Keys: 2,3 and e
        /*if (Input.GetKey("1"))
        {
            animator.SetInteger("Animation_Status", 3);
            animator.Play("guard_attack_b");
        }*/
        
        // If Key 2 is pressed and out pre-specified nexthit point in time has passed ...
        if (Input.GetKey("2") && Time.time >= hit_and_next_rates[0])
        {
            // ... set the hitrate to the next time point
            hit_and_next_rates[0] = Time.time + hit_and_next_rates[1];

            // ... play the animation for the attack
            animator.SetInteger("Animation_Status", 4);
            animator.Play("guard_attack_c");

            // If we are flipped then we know that we will have to flip the bullet as well
            if (gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                // Therefore the movement direction is set before to a fixed value ...
                shot.GetComponent<simple_moving>().movement_direction = -60f;
                // Then the sprite is flipped ...
                shot.GetComponent<SpriteRenderer>().flipX = true;
                // And the bullet is being instantiated with a small offset of -2 so that it spawns in front of our player
                Instantiate(shot, new Vector2(gameObject.transform.position.x - 2f, gameObject.transform.position.y), Quaternion.identity);
                // and then making a hit sound
                gameObject.GetComponent<AudioSource>().PlayOneShot(sound_effect, 1);
            }
            else
            {
                // Otherwise, we know that the player is not flipped, which means that we do not need to flip the bullet
                // Therefore we are setting the movement direction to another fixed value ...
                shot.GetComponent<simple_moving>().movement_direction = 60f;
                // We reset the flip in case it has been flipped before ...
                shot.GetComponent<SpriteRenderer>().flipX = false;
                // And last but not least the bullet is being instantiated again
                Instantiate(shot, new Vector2(gameObject.transform.position.x + 2f, gameObject.transform.position.y), Quaternion.identity);
                // and then making a hit sound
                gameObject.GetComponent<AudioSource>().PlayOneShot(sound_effect, 1);
            }
        }

        // In case the user presses 3 another attack is being performed -- just for cosmetic reasons and to try which attacks look the best 
        // This attack is the heavy attack and corresponds to two hits by the player in one
        if (Input.GetKey("3") && Time.time >= hit_and_next_rates[4])
        {
            hit_and_next_rates[4] = Time.time + hit_and_next_rates[5];
            animator.SetInteger("Animation_Status", 5);
            animator.Play("guard_attack_d");
        }
        
        ///// NOTE: I have implemented more attacks, but thought that three with different behaviour each, will be enough for the moment /////

        // This is going to be the default attack 
        // like before there is another check if the time from nexthit passed and if the hitrate has to be added on it again
        if (Input.GetKey("e") && Time.time >= hit_and_next_rates[2])
        {
            hit_and_next_rates[2] = Time.time + hit_and_next_rates[3];
            // If we can perform this attack, we are playing the animation
            animator.Play("guard_attack");
            // and then making a hit sound
            gameObject.GetComponent<AudioSource>().PlayOneShot(sound_effect, 1f);

            // NOTE NOT EVERY ATTACK HAS A SOUND BECAUSE I FOUND IT TO BE ANNOYING IT IS EASILY POSSIBLE TO ADD MORE DUE TO THE FLEXIBLE
            // IMPLEMENTATION WITH THE ARRAY, BUT I DO NOT THINK IT IS NECESSARY AT THIS POINT IN TIME AS THE CONCEPT OF WORKING WITH AUDIO
            // HAS ALREADY BEEN PROVEN BEFORE.
        }
        gameObject.GetComponent<player_score>().handleScore();
    }
}