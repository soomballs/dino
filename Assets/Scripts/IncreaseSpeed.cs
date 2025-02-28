using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Powerups/SpeedIncrease")]
public class IncreaseSpeed : PowerupEffect
{
    public override void apply()
    {
        GameManager.Instance.gameSpeedIncrease += 0.1f;
    }
}
