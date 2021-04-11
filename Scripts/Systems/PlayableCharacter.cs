using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayableCharacter : Character
{
    int originalAttack;
    public bool returnAttack = false;

    BattleSystem battleSystem;
    private void Start()
    {
        originalAttack = atkStat;
        battleSystem = FindObjectOfType<BattleSystem>();
    }
    private void Update()
    {
        healthText.text = currentHp + "/" + overallHp;
        attackText.text = "" + atkStat;
        defText.text = "" + defStat;
        if (damageNumberAppear == true)
        {
            damageNumber.transform.localPosition += new Vector3(0, 1.0f, 0);
        }
        else
        {
            damageNumber.gameObject.transform.localPosition = new Vector3(253, 166, 0);
        }

        if (healthNumberAppear == true)
        {
            healthNumber.transform.localPosition += new Vector3(0, 1.0f, 0);
        }
        else
        {
            healthNumber.gameObject.transform.localPosition = new Vector3(253, 166, 0);
        }

        if (incAtk == true)
        {

            increaseAttackStat.gameObject.transform.localPosition += new Vector3(0, 1.0f, 0);
        }
        else
        {
            increaseAttackStat.gameObject.transform.localPosition = new Vector3(253, 166, 0);
        }

        if (decAtk == true)
        {

            decreaseAttackStat.gameObject.transform.localPosition += new Vector3(0, 1.0f, 0);
        }
        else
        {
            decreaseAttackStat.gameObject.transform.localPosition = new Vector3(253, 166, 0);
        }


        if (atkStat < 0)
        {
            atkStat = 0;
        }

        if (currentHp <= 0)
        {
            if (this.tag == "Enemy")
            {
                battleSystem.enemyCharacters.Remove(this.gameObject);
            }
            else if (this.tag == "Ally")
            {
                battleSystem.playerCharacters.Remove(this.gameObject);
            }
            StartCoroutine(KillCharacter());
               
        }

        if(returnAttack == true)
        {
            IncreaseAttack(originalAttack - atkStat);
            atkStat = originalAttack;
            returnAttack = false;
        }
        /*For testing purposes:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }*/


    }

    IEnumerator KillCharacter()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }


}
