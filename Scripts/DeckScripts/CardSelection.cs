using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardSelection : MonoBehaviour
{
    /* List of all the cards
     * random-range pick a card
     * pick three cards
     * once picked they cannot reappear again/remove from list
     * button for next level/ aka you can skip
     */
    public SameDistanceChildren slots;
    public GameObject cardSelectionPrefab;

    public int slotNumber = 0;

    public List<CardAsset> allCards;
    int randomCardNumber;
    CardAsset chosenCard;

    string tutorialString;
    public Text tutorialText;
    public GameObject tutorialObject;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "TutorialCardSelectionScene")
        {
            StartCoroutine(CardSelectTutorial());
        }
        else
        {
            KeepingSameDeck.sceneNumber++;
        }
        StartCoroutine(SummonCards());
    }

    IEnumerator CardSelectTutorial()
    {
        tutorialString = "This is the Card Selection Scene!";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialString = "You will be asked to select a card of your choice from the ones that appear. You can choose a card or not select one at all.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialString = "The selected card will be added to your deck. Your customized deck will remain with you until you win or lose.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialString = "This is the end of the tutorial! Thank you for your patience. Return to the main menu to begin your playthrough!";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator SummonCards()
    {
        
        for (int i = 0; i < 3; i++)
        {
            randomCardNumber = Random.Range(0, allCards.Count);
            chosenCard = allCards[randomCardNumber];
            InstantiateGameObject(chosenCard);
            allCards.Remove(chosenCard);

            yield return new WaitForSeconds(2.0f);
        }
    }

    void InstantiateGameObject(CardAsset c)
    {
        cardSelectionPrefab.GetComponent<OneCardManager>().cardAsset = c;
        GameObject inHand = Instantiate(cardSelectionPrefab, transform.position, Quaternion.identity) as GameObject;
        SetSpace(inHand);

    }

    void SetSpace(GameObject go)
    {
        go.transform.SetParent(slots.Children[slotNumber].transform);
        go.transform.position = slots.Children[slotNumber].transform.position;
        slotNumber++;
    }

    public void DestroyOtherCards()
    {
       GameObject[] allCardsinScene = GameObject.FindGameObjectsWithTag("Cards");
        foreach(GameObject cardInScene in allCardsinScene)
        {
            if (cardInScene.GetComponent<BoxCollider>() == null)
            {
                Destroy(cardInScene.GetComponentInChildren<BoxCollider>());

            }
            else
            { Destroy(cardInScene.GetComponent<BoxCollider>()); }
        }
    }
}
