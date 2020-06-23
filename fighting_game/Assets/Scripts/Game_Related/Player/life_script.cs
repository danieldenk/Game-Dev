using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 *  This class handles the life functionality of the player
 */
public class life_script : MonoBehaviour
{
    // The player's lifes
    public int lifes;

    // The hearts that are being displayed and their sprite
    public Image[] lifes_displayed;

    // The attached sprite was a creative commons icon from the web
    public Sprite heart;

    // GameOver_Button and Exit_Button
    public Button button, button_x;

    // Sound for when the player dies
    public AudioClip died;

    // Start is called before the first frame update
    void Start()
    {
        // Initiation of the objects above
        GameObject.Find("Game_Over_UI").GetComponent<TextMeshProUGUI>().enabled = false;
        button = GameObject.Find("Game_Over_Button").GetComponent<Button>();
        button_x = GameObject.Find("Exit_Button").GetComponent<Button>();
        button.gameObject.SetActive(false);
        button_x.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        // If the player has no life left ...
        if (lifes <= -1)
        {
            // ... the player dies and falls off the screen
            Destroy(GameObject.Find("Player").gameObject.GetComponent<player_movement>());
            Destroy(GameObject.Find("Player").gameObject.GetComponent<Collider2D>(),1f);


            // ... Game Over is being displayed
            GameObject.Find("Game_Over_UI").GetComponent<TextMeshProUGUI>().enabled = true;

            // ... the buttons for restart and exit are being displayed
            button.gameObject.SetActive(true);
            button_x.gameObject.SetActive(true);

            // The animation of the dead player is being played + sound
            GameObject.Find("Player").gameObject.GetComponent<Animator>().Play("guard_dead");
            gameObject.GetComponent<AudioSource>().PlayOneShot(died, 1);

            // The spawners are being destroyed
            Destroy(GameObject.Find("EnemySpawner"),2f);
            Destroy(GameObject.Find("FoodSpawner"), 2f);

            // And a cool shake effect of the camera indicates that the game is over + music muting
            GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("shaking",true);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().mute = true;

            // saving the local score for later appendement
            int local_score = gameObject.GetComponent<player_score>().score;

            // If the gained score of the player has been a new highscore ...
            // ... the new highscore is being overwritten
            if (local_score > score_saver.LoadState())
                score_saver.SaveState(local_score);
          
            // Otherwise / And after that both values are being displayed to motivate the player to play again
            GameObject.Find("Highscore_Text").gameObject.GetComponent<TextMeshProUGUI>().SetText("YOUR SCORE: " + local_score + "\nHIGHSCORE: "+ score_saver.LoadState());
        }
    }
    // This method is being used to draw the lifes within the health bar
    public void drawLife()
    {
        // Quickly enabling all of the lifes that are left
        // Is being called here for better performance
        // Makes sure that when we pick up another life, that it is being displayed (and if we lose one as well)
        for (int i = 0; i < lifes_displayed.Length; i++)
            if (i < lifes)
                lifes_displayed[i].enabled = true;
    }
    // Whenever our player gets hit, we have to draw the healthbar
    public void OnCollisionEnter2D(Collision2D collision)
    {
        drawLife();
    }
}