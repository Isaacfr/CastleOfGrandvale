using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BattleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<GameObject> enemyCharacters;
    public List <GameObject> playerCharacters;

    GameObject playerTarget;
    GameObject enemyTarget;

    [SerializeField]
    int[] playerDamageValues;
    [SerializeField]
    int[] enemyDamageValues;

    Deck deck;
    public GameObject nextLevelButton;

    bool enemyAttacking;
    bool alliesAttacking;
    bool drawingCards;

    public CardAsset ca;

    public GameObject endTurnButton;

    void Start()
    {
        nextLevelButton.SetActive(false);
        deck = Object.FindObjectOfType<Deck>();
        /*
        enemyCharacters = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyCharacters)
        {
            if (enemy.GetComponent<PlayableCharacter>().id == 3)
            {
                enemyCharacters[2] = enemy;
            }
            
            else if (enemy.GetComponent<PlayableCharacter>().id == 1)
            {
                enemyCharacters[0] = enemy;
            }
            else if (enemy.GetComponent<PlayableCharacter>().id == 2)
            {
                enemyCharacters[1] = enemy;
            }
            
        }

        playerCharacters = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject player in playerCharacters)
        {
            if (player.GetComponent<PlayableCharacter>().id == 1)
            {
                playerCharacters[0] = player;
            }
            else if (player.GetComponent<PlayableCharacter>().id == 2)
            {
                playerCharacters[1] = player;
            }
            else
            {
                playerCharacters[2] = player;
            }
        }*/
    }

    void Update()
    {
        if (enemyCharacters.Count == 0)
        {

            deck.text.text = "You Win!";
            deck.text.gameObject.SetActive(true);
            nextLevelButton.SetActive(true);
            /*
            StartCoroutine(GainACard());
            */
        }
        else if(playerCharacters.Count == 0)
        {
            deck.text.text = "You lose...";
            deck.text.gameObject.SetActive(true);
            //reset button/ exit button
        }
    }

    public void EndTurnButton()
    {
        enemyAttacking = true;
        alliesAttacking = false;
        drawingCards = false;
        endTurnButton.SetActive(false);
        deck.RemoveCardsFromHand();
        StartCoroutine(DoMethods());
        
    }
    IEnumerator DoMethods()
    {
        if (enemyAttacking == true)
        {
            deck.AppearText("Enemies are attacking!", 6.0f);
            EnemiesAttack();
            yield return new WaitForSeconds(6.0f);
            enemyAttacking = false;
            alliesAttacking = true;
        }
        

        if (alliesAttacking == true)
        {
            deck.AppearText("Allies are retaliating!", 6.0f);
            AlliesAttack();
            yield return new WaitForSeconds(6.0f);
            alliesAttacking = false;
            drawingCards = true;
        }

        if (drawingCards == true)
        {
            deck.AppearText("Drawing cards...", 8.0f);

            ReplenishAndDraw();
            yield return new WaitForSeconds(8.0f);
            drawingCards = false;
        }

        yield return new WaitForSeconds(3.0f);
        endTurnButton.SetActive(true);

    }

    void EnemiesAttack()
    {
        int enemyValue = 0;
        enemyDamageValues = new int[enemyCharacters.Count];
        foreach (GameObject enemy in enemyCharacters)
        {
            enemyDamageValues[enemyValue] = enemy.GetComponent<PlayableCharacter>().atkStat;
            enemyValue++;
        }

        StartCoroutine(WaitForDamage(playerTarget, enemyDamageValues, playerCharacters));
        
    }

    void AlliesAttack()
    {
        int playerValue = 0;
        playerDamageValues = new int[playerCharacters.Count];
        foreach (GameObject player in playerCharacters)
        {
            playerDamageValues[playerValue] = player.GetComponent<PlayableCharacter>().atkStat;
            playerValue++;
        }
        

        StartCoroutine(WaitForDamage(enemyTarget, playerDamageValues,enemyCharacters));
        
    }

    IEnumerator WaitForDamage(GameObject target, int[] damageValues, List <GameObject> characters)
    {
        foreach (int damage in damageValues)
        {
            yield return new WaitForSeconds(2.0f);
            if(characters[0] == null && characters[1] == null)
            {
                target = characters[2];
            }
            else if(characters[0] == null)
            {
                target = characters[1];
            }
            else 
            {
                target = characters[0];
            }
            target.GetComponent<PlayableCharacter>().TakeDamage(damage);

        }
    }

    void ReplenishAndDraw()
    {
        deck.DrawCards();
        foreach(GameObject enemy in enemyCharacters)
        {
            enemy.GetComponent<PlayableCharacter>().returnAttack = true;
        }
    }

    /*testing static class + method

    IEnumerator GainACard()
    {
            KeepingSameDeck.AddNewCards(ca);
            yield return new WaitForSeconds(1.0f);
        
    }*/
}
