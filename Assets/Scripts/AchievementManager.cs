using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] Animator achievementScore200;
    [SerializeField] Animator deathAchievement;
    [SerializeField] Animator achievementScore500;
    [SerializeField] Animator achievementScore1000;
    [SerializeField] GameManager gameManager;
    private bool score200state = false;
    private bool deathState = false;
    private bool score500state = false;
    private bool score1000state = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.getScore() >= 200f && score200state == false)
        {
            achievementScore200.SetTrigger("Appear");
            score200state = true;
        }

        if (gameManager.getGameOver() == true && deathState == false)
        {
            Debug.Log("first death");
            deathAchievement.SetTrigger("Appear");
            deathState = true;
        }

        if (gameManager.getScore() >= 500f && score500state == false)
        {
            achievementScore500.SetTrigger("Appear");
            score500state = true;
        }

        if (gameManager.getScore() >= 1000f && score1000state == false)
        {
            achievementScore1000.SetTrigger("Appear");
            score1000state = true;
        }
    }
}
