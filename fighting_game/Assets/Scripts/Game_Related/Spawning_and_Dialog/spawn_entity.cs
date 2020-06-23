using UnityEngine;

/**
 * This is the class that is being used to spawn objects into the game.
 * I am using it for food and enemies
 */
public class spawn_entity : MonoBehaviour 
{
    // This array includes all of the obejcts that are to be spawned
    public GameObject[] objects;

    // This is a buffer area that makes sure that enemies do not spawn too close to the player
    public float epsilon = 1.5f;

    // These are the coordinates that define the area of spawning
    // 1 -> from barrier left to the players location
    // 2 -> from barrier right to the players location
    private float x_coord1, x_coord2;

    // based on the coordinates above here the location is defined
    private Vector2 spawn_location;

    // This is the duration in what interval enemies are being spawned
    public float spawning_duration = 7f;

    // This is the time that is indicating how long we have to wait til the next object is being spawned
    public float spawn_time_left = 3f;

    // True if spawning enemy | False if spawning food
    public bool isEnemy = true;

    // The player's position
    private Vector2 playerPosition;

    // The limitations of the world map (spawning boundaries left and right)
    private float[] WorldMapLimitations = { -5f, 5f };

    // Update is called once per frame
    void Update()
    {
        // Getting the players position within the game -- needed for spawning logic
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // basically checking if the real time has surpassed the time we intended to wait
        if (Time.time > spawn_time_left)
        {
            // then we tell the script to wait another x seconds before the code beneath can be executed again
            spawn_time_left = spawning_duration + Time.time;

            // setting above coordinates
            x_coord1 = Random.Range(WorldMapLimitations[0], playerPosition.x - epsilon);
            x_coord2 = Random.Range(playerPosition.x + epsilon, WorldMapLimitations[1]);

            // Creating a 50-50 Possibility for an object to spawn on the left/right side of the player
            spawn_location = Random.Range(0f, 1f) >= 0.5 ? new Vector2(x_coord1, playerPosition.y) : spawn_location = new Vector2(x_coord2, playerPosition.y);

            // PLEASE NOTE: The split has to be done in order to include out set epsilon value!

            // If we are spawning enemies ...
            if (isEnemy)
            {
                // ... then we need to spawn specific enemies from our objects array
                Instantiate(objects[0], spawn_location, Quaternion.identity);
                Instantiate(objects[2], spawn_location, Quaternion.identity);

                // ... for the player to have a tougher fight, the last enemy category is only being spawned when the player has surpassed 2500 in score
                // This could theoretically also be modelled as a unity event. I just think this way its easier to understand it and the code does what its supposed to. 
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<player_score>().score >= 2500) 
                    Instantiate(objects[1], new Vector2(-spawn_location.x, spawn_location.y + 1.75f), Quaternion.identity);
            }
            // In case that food is being spawned
            else
            {
                // A random item is being spawned 
                Instantiate(objects[Random.Range(0, 3)], spawn_location, Quaternion.identity);
            }
        }
    }
}