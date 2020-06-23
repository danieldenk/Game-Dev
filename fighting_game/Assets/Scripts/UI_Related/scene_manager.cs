using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This is the class that we are using to navigate between the scenes within the options menu
 */
public class scene_manager : MonoBehaviour
{
    // The sound that is being played when a button is clicked
    public AudioClip click_sound;

    // This is the method for closing the game itself
    public void exit()
    {
        Application.Quit();
    }

    // With this method we can pass the sceneName as a parameter and load a scene
    // GameScene: called to restart the game / load scene one
    // StoryScene: called to start the tutorial and story of the game (Scene 2)
    // Options: called to load the options scene where we can choose between starting the game and watching the tutorial scene
    public void loadScene(string sceneName)
    {
        // making a sound when the button is clicked
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(click_sound, 1);
        // loading the scene
        SceneManager.LoadScene(sceneName);
    }
}