using UnityEngine;

/**
 * This class includes the logic for the shot fired by the player and how to handle a collision with an enemy
 */
public class shoot : MonoBehaviour
{
     void Update()
    {
        // If nothing is hit, then the shot is being destroyed after 2 seconds
        Destroy(this.gameObject, 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the player hits an enemy it is losing a hitpoint, then the shot is being destroyed after two seconds
        if (collision.gameObject.tag.Equals("Enemy"))
            collision.gameObject.GetComponent<enemy_script>().hits--;
    }
}