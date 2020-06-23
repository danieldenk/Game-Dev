using UnityEngine;

/**
 * This is the class that is being used to make the infobox (dis-)appear whenever the 
 * ?-button within the options scene is being clicked. 
 */
public class start_infobox : MonoBehaviour
{
    // This object contains all of the components that are including the infobox elements
    [SerializeField] 
    private GameObject box;

    void Start()
    {
        // We are finding the infobox element and are setting it to not-active so it does not appear above our other elements
        box = GameObject.Find("Infobox").gameObject;
        box.SetActive(false);
    }

    /**
     * Method for displaying the infoxbox in screen
     */
    public void showBox()
    {
        box.SetActive(true);
    }

    /**
     * Method for closing the infobox on the screen 
     */
    public void closeBox()
    {
        box.SetActive(false);
    }
}
