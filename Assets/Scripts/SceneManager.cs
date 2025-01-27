using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneSwitcher (int i)
    {
        if (i == 0) { }
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
        if (i == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
        }
        if (i == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("UpdatedGame");
        }
    }
}
