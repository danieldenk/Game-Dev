using System.Collections;
using UnityEngine;
using TMPro;

/**
 * This class is containing all of the methods needed to display a conversation between characters in the game
 * It furthermore takes care of the cinematic movement if the camera
 * 
 * The idea of implementing the dialog system in this kind of way was inspired through Blackthornprod  
 * (https://www.youtube.com/watch?v=f-oSXg6_AMQ&t=436s Last accessed: 30.04.20)
 * I did not copy it, I just thought his way to approach this problem is interesting so I created my own
 * solution that is also making use of displaying characters one by one.
 */
public class dialog_script : MonoBehaviour
{
    // The object where the text is going to be displayed at
    public TextMeshProUGUI txt_display;
    // This array contains all of the dialog
    public string[] dialog;
    // used to indicate how many sentences are left
    private int i;
    // player Transform object
    public Transform player;
    // The offset from the character to the camera -- makes sure that only map is visible
    public Vector3 offset;
    // The camera that is being moved
    public GameObject cameraSelected;

    void Start()
    {
        // Initiating the camera
        cameraSelected = GameObject.Find("Camera");
    }
    void Update()
    {
        // Setting the camera to the players position while making sure it has an offset and displays all of the items (indicated by -5)
       cameraSelected.transform.position = new Vector3(player.position.x + offset.x, cameraSelected.transform.position.y, -5);

        // If everything has been said ...
        if (i >= dialog.Length)
        {
            // ... flip the second player so he faces right
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            // ... make him move to the right
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(45f, gameObject.GetComponent<Rigidbody2D>().velocity.y)*Time.deltaTime;
            // ...set his move animation
            gameObject.GetComponent<Animator>().SetInteger("Animation_Status", 1);
            GameObject.Find("Dialog_Text").GetComponent<TextMeshProUGUI>().SetText("Click the Button above to start the Game - Walk with 'W & D' and attack with 'E and 2 or 3'. ");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If a collision with the player is occuring then we start the displaying of the dialog
        if (collision.gameObject.tag == "Player")
            StartCoroutine(Type());
    }
    IEnumerator Type()
    {
        // For every sentence in the dialog array
        foreach (string sentence in dialog)
        {
            // For every character that is within a sentence
            foreach(char c in sentence.ToCharArray())
            {
                // The text is being appended one more character
                txt_display.text += c;
                // Before waiting 0.1 seconds
                yield return new WaitForSeconds(0.1f);
            }
            // After every sentence we have to wait 6.3 seconds for it to be set back to nothing
            yield return new WaitForSeconds(6.3f);
            txt_display.text = "";
            // Then i is incremented so we know that the next sentence has started
            ++i;
        }
    }
}