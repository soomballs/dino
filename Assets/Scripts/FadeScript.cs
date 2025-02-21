using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    [SerializeField] TMP_Text splash;
    [SerializeField] string scene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Fade()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Fading in");
            splash.GetComponent<TMP_Text>().CrossFadeAlpha(50.0f, 2.0f, false);
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("fading out");
            splash.GetComponent<TMP_Text>().CrossFadeAlpha(0.01f, 2.0f, false);
            yield return new WaitForSeconds(1.0f);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
