using UnityEngine;

/**
 * This is the class containing the logic that is needed by the enemy to die and to live
 */
public class enemy_script : MonoBehaviour
{
    // Gets us the coordinates of the player in a later point within the script
    private Transform target;

    // These are variables that are only relevant to the spider because it can jump and flies through the air to hit the player
    public float speed = 0.1f;
    public float jumpforce=1;

    // Indicates how many hits the enemy can take until it dies
    public int hits=1;

    // An array containing the sound effects that are being played by the enemy
    // [0] => Enemy has been hit Sound
    // [1] => Enemy Destroyed Sound
    // [2] => Player HIT Sound
    public AudioClip[] sound_effects;

    // This is the death prefab that is being instantiated whenever the enemy has died
    public GameObject death;

    // Start is called before the first frame update
    void Start()
    {
        //  Rigidbody rotation stop and Initiation of the player transform object
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // The direction indicates which way the enemy is facing ...
        Vector3 direction = target.position - transform.position;

        // ... if we are facing to the left then we have to flip the x coordinate of the enemy sprite so it faces towards the player (right side)
        // ... if we are facing to the left then we have to unflip the x coordinate of the enemy sprite so it faces towards the player (left side)
        gameObject.GetComponent<SpriteRenderer>().flipX = direction.x >= 0 ? true : false;

        // Here we are telling the enemy in which direction it has to move in order to reach the player
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y + jumpforce), target.position, speed * Time.deltaTime);

        // if the enemy does not have any more hits left, then the enemy dies and has to be removed from the scene
        if (hits <= 0)
        {
            // instantiating the death prefab animation
            Instantiate(death, transform.position, Quaternion.identity);

            // the player gets points for every enemy he destroyed
            GameObject.Find("Player").gameObject.GetComponent<player_score>().score += 100;

            // The Enemy dies while playing a DESTROYED sound
            gameObject.GetComponent<AudioSource>().PlayOneShot(sound_effects[1], 1);
            Destroy(this.gameObject);
        }
    }
}