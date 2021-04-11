using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Slider healthSlider;
    public Text healthText;
    public Text attackText;
    public Text defText;
    //change into find component under the character

    public int overallHp;
    public int currentHp;

    public int atkStat;
    public int defStat;

    public bool damageNumberAppear = false;
    public bool healthNumberAppear = false;

    public Text damageNumber;
    public Text healthNumber;

    public int id;

    public Text increaseAttackStat;
    public Text decreaseAttackStat;

    public bool incAtk = false;
    public bool decAtk = false;

    private void Awake()
    {
        SetStats();
    }


    public void SetStats()
    {
        healthSlider.maxValue = overallHp;
        healthSlider.value = currentHp;
        healthText.text = currentHp + "/" + overallHp;
        attackText.text = "" + atkStat;
        defText.text = "" + defStat;
    }
    public void TakeDamage(int damage)
    {
        DamageAppear(damage);

        currentHp -= damage;
        healthSlider.value = currentHp;

       
    }

    public void IncreaseAttack(int attackIncrease)
    {
        atkStat += attackIncrease;
        increaseAttackStat.text = "+" + attackIncrease;
        increaseAttackStat.gameObject.SetActive(true);
        increaseAttackStat.gameObject.transform.localPosition = new Vector3(253, 166, 0);
        incAtk = true;

        StartCoroutine(WaitandDisappear(2.0f, increaseAttackStat.gameObject, incAtk));
        
    }
    public void DecreaseAttack(int attackDecrease)
    {
        atkStat -= attackDecrease;
        decreaseAttackStat.text = "-" + attackDecrease;
        decreaseAttackStat.gameObject.SetActive(true);
        decreaseAttackStat.gameObject.transform.localPosition = new Vector3(253, 166, 0);
        decAtk = true;

        StartCoroutine(WaitandDisappear(2.0f, decreaseAttackStat.gameObject, decAtk));
    }

    public void HealDamage(int amount)
    {
        currentHp += amount;
        healthSlider.value = currentHp;
        if (currentHp > overallHp)
        {
            currentHp = overallHp;
            amount = overallHp - currentHp;
        }
        healthNumber.text = "+" + amount;
        healthNumber.gameObject.SetActive(true);
        healthNumber.gameObject.transform.localPosition = new Vector3(253, 166, 0);
        healthNumberAppear = true;

        StartCoroutine(WaitandDisappear(2.0f, healthNumber.gameObject, healthNumberAppear));
    }


    public void DamageAppear(int damage)
    {
        damageNumber.text = "-" + damage;
        damageNumber.gameObject.SetActive(true);
        damageNumber.gameObject.transform.localPosition = new Vector3(253, 166, 0);
        damageNumberAppear = true;
        StartCoroutine(WaitandDisappear(2.0f, damageNumber.gameObject, damageNumberAppear));

    }

    IEnumerator WaitandDisappear(float number, GameObject ga, bool a)
    {
        yield return new WaitForSeconds(number);
        ga.SetActive(false);
        a = false;
    }
    /*public void IncreaseDefense(int defenseIncrease)
    {
        defStat += defenseIncrease;
    }
    public void DecreaseDefense(int defenseDecrease)
    {
        defStat -= defenseDecrease;
    }*/

}
