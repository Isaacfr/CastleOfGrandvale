using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    private void Update()
    {
        healthText.text = currentHp + "/" + overallHp;
        attackText.text = "" + atkStat;
        defText.text = "" + defStat;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }
}
