using UnityEngine;

/**
 * This class is being used to toggle the music within the game
 */
public class toggle_music : MonoBehaviour
{
    // Checking if the music is playing based on this variable
    bool musicPlaying = false;

    // This is the gameobject where the music is being played from
    public Camera source;

    public void toggleMusic()
    {
        // Muting the music if it is playing and unmuting it elsewise
        musicPlaying = !musicPlaying;
        source.GetComponent<AudioSource>().mute = musicPlaying;
    }
}
