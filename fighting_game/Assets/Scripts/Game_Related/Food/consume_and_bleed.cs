using UnityEngine;

/**
 * This is the class containing the logic for every item that is being spawned by the food spawner
 * I am also using it to destroy the particles from the particle prefab because its basically the same logic and it removes redundancy
 */
public class consume_and_bleed : MonoBehaviour
{
    // setiing a standard destroy length of 5 sec
    public float destroy_length  = 5f;

    // if we are talking about a consumable food item and not the particle system
    public bool isNotParticle = true;

    // Sound played if consumed by player
    public AudioClip consumed;

    // Update is called once per frame
    void Update()
    {
        // every item is only collectable for a duration of 5 seconds
        // the particles are only visible for 5 seconds
        Destroy(gameObject, destroy_length);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      // if we arent looking at the particles from the particle system, but a food item
        if (isNotParticle)
            // If the player walks through the item then the consumed sound is played
            if (collision.gameObject.tag.Equals("Player"))
            {
                player_score score = collision.gameObject.GetComponent<player_score>();

                // Here different situations of pickups are being handled. Because I am German, I have tried to give it a more 
                // personal touch and therefore included some of the main German dishes. 

                // If a player is picking up a pretzel then he is gaining extra score and the pickup sound is played.
                if (gameObject.tag.Equals("Pretzel"))
                {
                    Debug.Log("Pretzel consumed");
                    score.score += 50;
                }
                // If a player is picking up a sausage then he is gaining extra score and the pickup sound is played.
                // Plus the player's walking speed increases permanently --> Powerup
                if (gameObject.tag.Equals("Sausage"))
                {
                    Debug.Log("Sausage consumed");

                    if (collision.gameObject.GetComponent<player_movement>().speed < 2.5f)
                    {
                        collision.gameObject.GetComponent<player_movement>().speed += 0.25f;
                        score.score += 50;
                    }
                    else
                        score.score += 150;
                }
                // If a player is picking up a beer then he is gaining extra score and the pickup sound is played.
                // Plus the player is being given an extra life --> Buff
                if (gameObject.tag.Equals("Beer"))
                {
                    Debug.Log("Beer consumed");
                    if (collision.gameObject.GetComponent<life_script>().lifes < 5)
                        collision.gameObject.GetComponent<life_script>().lifes += 1;
                    score.score += 250;
                    collision.gameObject.GetComponent<life_script>().drawLife();
                }
                gameObject.GetComponent<AudioSource>().PlayOneShot(consumed, 1);
                Destroy(gameObject, consumed.length);
            }
                // Else: In case if enemy collides with food do nothing // Better for computing power?
                // Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
    }
}