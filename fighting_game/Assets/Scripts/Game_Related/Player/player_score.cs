using UnityEngine;
using TMPro;

/*
 * This is the responsible class for keeping the player's score
 */
public class player_score : MonoBehaviour
{
    // The score of the player
    public int score = 0;

    // Flags so we know if we have reeached a certain score point
    // based on that certain behaviour is going to be initiated -> more enemies
    private bool flag_score_1k = true, flag_score_2k = true;

    // The text containing the score
    public TextMeshProUGUI Scoreboard;

    // This function handles the score gained by the player and triggers custom behavior if a score threshold is surpassed
    public void handleScore()
    {
        // The Scoretext is being updated based on the new score
        // Due to performance reasons this might also be called within an event like trigger as well, instead of every frame 
        Scoreboard.text = "Score: " + score;

        // That is the advanced functionality I tried to tease on before at the beginning of this script
        // If we reach a score that is higher than 1k and equal or below 2k, then we are setting the flag and
        // decrease the spawntime of the enemies for an extra difficulty
        if (score > 1000 && score <= 2000 && flag_score_1k)
        {
            flag_score_1k = false;
            GameObject.Find("EnemySpawner").GetComponent<spawn_entity>().spawning_duration -= 2.5f;
        }
        // The same as above 
        else if (score > 2000 && score <= 3000 && flag_score_2k)
        {
            flag_score_2k = false;
            GameObject.Find("EnemySpawner").GetComponent<spawn_entity>().spawning_duration -= 2.5f;
        }
    }
}