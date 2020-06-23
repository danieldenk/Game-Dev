using System;
using UnityEngine;

/* 
 * This is the class that is responsible for the attack of an enemy
 */
public class enemy_attack : MonoBehaviour
{
    // An array containing the sound effects that are being played by the enemy
    // [0] => Enemy has been hit Sound
    // [1] => Enemy Destroyed Sound
    // [2] => Player HIT Sound
    private AudioClip[] sound_effects;

    // The particle system that is being spawned whenever the enemy has been hit
    public GameObject blood;

    // These are the recognized attacks, which will be detected by enemies
    string[] attacks = { "guard_attack", "guard_attack_b", "guard_attack_c", "guard_attack_d" };

    // These timestamps are needed to make sure that the oncollisionstay function takes longer to process hits
    public float timeRequired = .5f;
    float passedTime;

    public void Start()
    {
        // Getting the sound effects from the enemy script to reduce redundancy, as anemey script is containing all of the variables that make an enemy
        sound_effects = gameObject.GetComponent<enemy_script>().sound_effects;
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        // If a collision has been detected that involves the player (PLAYER BUMPED INTO ENEMY)
        // >>AND<< the player has been within a state of animation, then it means that the player initiated an attack against the enemy
        // --> The attacks are listed below
        if (col.gameObject.tag.Equals("Player"))
        {
            bool isAttacking = Array.Exists(attacks, attack => col.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(attack));

            // If we are attacking an enemy and one of above attack animations is played then ...
            if (isAttacking)
                handleBeingAttacked(col);

            // If the PLAYER HAS BEEN HIT (no animation played or idle/walking) then the player has to lose health
            else
                handleAttack(col);
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            bool isAttacking = Array.Exists(attacks, attack => col.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(attack));
            // If we are attacking an enemy and one of above attack animations is played AND we are ready to notice an attack => Call attack function
            passedTime += Time.fixedDeltaTime;
            if (passedTime > timeRequired)
                if (isAttacking)
                    handleBeingAttacked(col);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset timestamp back to 0 for the next collision
        if (collision.gameObject.tag.Equals("Player"))
            passedTime = 0;
    }
    // Method for handling an attack on the enemy
    public void handleBeingAttacked(Collision2D col)
    {
        // ...  we have to instantiate the blood particle prefab and decrease the hitpoints of the enemy
        Instantiate(blood, transform.position, Quaternion.identity);
        gameObject.GetComponent<enemy_script>().hits--;

        // and we also have to play the hitsound of the PLAYER ATTACKING the ENEMY
        gameObject.GetComponent<AudioSource>().PlayOneShot(sound_effects[0], 1);

        // If the player is executing a heavy attack then the enemy takes in an additional hit
        if(col.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("guard_attack_d"))
            gameObject.GetComponent<enemy_script>().hits--;
    }
    // Method for handling an attack on the player
    public void handleAttack(Collision2D col)
    {
        // The hit sound for the player is being played ...
        gameObject.GetComponent<AudioSource>().PlayOneShot(sound_effects[2], 1);

        // ... then the player loses a life
        col.gameObject.GetComponent<life_script>().lifes -= 1;

        // ... the corresponding heart on the top left is removed as well -- [-1 because the last heart displayed is counting as well]
        if (col.gameObject.GetComponent<life_script>().lifes > -1)
            col.gameObject.GetComponent<life_script>().lifes_displayed[col.gameObject.GetComponent<life_script>().lifes].enabled = false;

        // ... the player gets a hit animation
        col.gameObject.GetComponent<Animator>().Play("guard_hit");
    }
}