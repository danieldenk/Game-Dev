using UnityEngine;
using System.IO;

/**
 * Since the other functionality regarding the scores is static and does not derive from mono behaviour a delete functionality in its own script,
 * has to be provided as well
 */
public class reset_score : MonoBehaviour
{
    // sound of button click
    public AudioClip click_sound;

    // Method for deleting the highscore
    public void DeleteState()
    {
        // Path to the highscore file
        string path = Application.persistentDataPath + "/Highscore.csv";

        // Playing button clicked sound
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(click_sound, 1);

        try
        {
            // We try to delete it, if it exists
            File.Delete(path);
            Debug.Log("File (Score) deleted");
        }
        catch
        {
            // If the file does not exists then we do not need to delete it
            // In this case we do nothing
            Debug.Log("File (Score) didnt exists.");
        }
        // Removing the button from the scene as a response that shows we did sth
        Destroy(this.gameObject);
    }
}