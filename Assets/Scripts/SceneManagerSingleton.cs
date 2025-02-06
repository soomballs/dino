using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerSingleton : MonoBehaviour
{
    public static SceneManagerSingleton Instance {get ; private set;}
    

    void Awake() {
        if(Instance == null) {
            Instance = this;

            DontDestroyOnLoad(gameObject);
            
        } else {
            Destroy(gameObject);
        }

    }

    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
 }
