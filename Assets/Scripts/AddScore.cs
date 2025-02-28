using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
[CreateAssetMenu(menuName ="Powerups/ScoreIncrease")]
public class AddScore : PowerupEffect
{
    public float amount;
    public override void apply()
    {
        GameManager.Instance.addScore(amount);
    }
}
