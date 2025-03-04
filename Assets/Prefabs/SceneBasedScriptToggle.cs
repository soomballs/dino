using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBasedScriptSwitcher : MonoBehaviour
{
    public MonoBehaviour oldGame;  // First script
    public MonoBehaviour newGame;  // Second script

    public string sceneForOldGame; // Scene where Script A should be enabled
    public string sceneForNewGame; // Scene where Script B should be enabled

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == sceneForOldGame)
        {
            oldGame.enabled = true;
            newGame.enabled = false;
        }
        else if (currentScene == sceneForNewGame)
        {
            oldGame.enabled = false;
            newGame.enabled = true;
        }
        else
        {
            // Default: disable both if the scene isn't listed
            oldGame.enabled = false;
            newGame.enabled = false;
        }
    }
}
