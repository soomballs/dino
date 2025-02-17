
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class ButtonUsage : MonoBehaviour
{
    public string sceneToLoad;
    private AudioSource buttonAudio;

    private SceneManagerSingleton buttonInstance;


     
    void Start()
    {
        buttonAudio = GetComponent<AudioSource>();
        buttonInstance = SceneManagerSingleton.Instance;
    }

    public void ChangeScene()
    {
        if (buttonInstance != null && buttonAudio != null)
        {

            buttonAudio.Play();
            //delay until audio is finished. It's async though
            StartCoroutine(WaitForSoundAndChangeScene(buttonAudio.clip.length));
        }
        else
        {
            
            Debug.Log("The scene Changer isn't here.");
        }
    }

    public void quitApp() {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private IEnumerator WaitForSoundAndChangeScene(float soundDuration) {
        yield return new WaitForSeconds(soundDuration);

        buttonInstance.ChangeScene(sceneToLoad);

    }

}
