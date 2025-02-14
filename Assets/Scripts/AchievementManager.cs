using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] Animator achievement;
    [SerializeField] Animator deathAchievement;
    [SerializeField] GameManager gameManager;
    private bool state = false;
    private bool deathState = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.getScore() >= 200f && state == false)
        {
            achievement.SetTrigger("Appear");
            state = true;
        }

        if (gameManager.getGameOver() == true && deathState == false)
        {
            Debug.Log("first death");
            deathAchievement.SetTrigger("Appear");
            deathState = true;
        }
    }
}
