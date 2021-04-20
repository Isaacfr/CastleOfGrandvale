using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Deck : MonoBehaviour
{

    public List<CardAsset> cards = new List<CardAsset>();
    
    private List<CardAsset> discards = new List<CardAsset>();
    
    int cardsAmount;
    int discardsAmount;
    public SameDistanceChildren slots;

    public GameObject singleTargetPrefab;
    public GameObject multipleTargetPrefab;

    public Text text;

    string tutorialString;
    public Text tutorialText;
    public GameObject tutorialObject;
    
    public int slotNumber = 0;

    [SerializeField]
    GameObject[] daCards;
    [SerializeField]
    bool draggable;

    public bool checkDeck;

    public GameObject endTurnButton;
    public GameObject tutorialEndTurnButton;

    BattleSystem battleSystem;
    void Awake()
    {

        if (SceneManager.GetActiveScene().name == "Scene9")
        {
            foreach (CardAsset cA in cards)
            {
                KeepingSameDeck.sameDeck.Add(cA);
            }
            cards.Shuffle();
            AppearText("Drawing cards...", 8.0f);
        }
        else if (SceneManager.GetActiveScene().name == "TutorialScene")
        {

        }
        else if(SceneManager.GetActiveScene().name != "Scene9" && SceneManager.GetActiveScene().name != "TutorialScene")
        {
            cards = KeepingSameDeck.sameDeck;
            cards.Shuffle();
            AppearText("Drawing cards...", 8.0f);
        }
        

    }

    private void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
        if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            StartCoroutine(TutorialDeck());
            cardsAmount = cards.Count;
            endTurnButton.SetActive(false);
            tutorialEndTurnButton.SetActive(false);

        }
        else
        {
            tutorialText = null;
            cardsAmount = cards.Count;
            endTurnButton.SetActive(false);
            Invoke("DrawCards", 2.0f);
        }
    }

    private void Update()
    {
        Debug.Log("here " + KeepingSameDeck.sameDeck.Count);
        daCards = GameObject.FindGameObjectsWithTag("Cards");
        if (draggable == false)
        {
            foreach (GameObject daCard in daCards)
            {
                if (daCard.transform.GetComponent<BoxCollider>() == null)
                {
                    daCard.transform.GetComponentInChildren<BoxCollider>().enabled = false;
                }
                else
                {
                    daCard.transform.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
        else
        {
            foreach (GameObject daCard in daCards)
            {
                if (daCard.transform.GetComponent<BoxCollider>() == null)
                {
                    daCard.transform.GetComponentInChildren<BoxCollider>().enabled = true;
                }
                else
                {
                    daCard.transform.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
        Debug.Log(daCards.Length);
        if (battleSystem.enemyCharacters.Count == 0 || battleSystem.playerCharacters.Count == 0)
        {
            draggable = false;
        }

    }

    public void BattlePhaseTutorial()
    {
        StartCoroutine(Tutorial2());
    }

    public void CardSelectionPhase()
    {
        StartCoroutine(Tutorial3());
    }
    IEnumerator TutorialDeck()
    {
        tutorialObject.SetActive(true);
        tutorialString = "Welcome! This is Castle of Grandvale, a deck-building, adventure card game!";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialString = "In this game, you control a group of warriors who need to advance to the Castle of Grandvale!";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialString = "You win a match once you defeat all enemies by dropping their health to 0. You lose once all your allies are defeated by having their health go down to 0.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(529f, 0);
        tutorialString = "Each character has their own health, name and attack stat.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-529f, 0);
        tutorialString = "Enemies will have their own health, name and attack stat.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 184f);
        tutorialString = "There are 3 phases in a round: the Draw Phase, the Card Phase and the Battle Phase.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        AppearText("Drawing Cards...", 8.0f);
        Invoke("DrawCards", 0.1f);
        tutorialString = "In the Draw Phase, you will draw a 6 cards from your deck. You will then begin the Card Phase";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialString = "In the Card Phase, you can play the cards that you draw. Once you play your cards, they will go to a discard pile. You will draw new cards at the start of your next Draw Phase.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);
        tutorialObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 309f);
        tutorialString = "There are two forms of cards. Some cards allow you to target certain characters. Others affect multiple targets. Play the cards onto the field to use them.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        draggable = true;

        tutorialEndTurnButton.SetActive(true);
    }
    IEnumerator Tutorial2()
    {
        tutorialEndTurnButton.SetActive(false);
        tutorialObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -329f);
        tutorialString = "Once you press the End Turn Button, the Battle Phase Begins.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

        tutorialString = "In the Battle Phase, characters will attack each other automatically. The closest character to the middle will be struck first. Enemies attack before allies do.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);
        tutorialObject.SetActive(false);

    }

    IEnumerator Tutorial3()
    {
        tutorialObject.SetActive(true);
        tutorialString = "The Battle Phase is now conducted. The round starts a new with a new Draw Phase. Let's proceed to the Card Selection Scene.";
        for (int i = 0; i <= tutorialString.Length; i++)
        {
            tutorialText.text = tutorialString.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2.0f);

    }

    public void EndButtonDeck()
    {
        KeepingSameDeck.sameDeck = cards;
        foreach (CardAsset da in discards)
        {
            KeepingSameDeck.sameDeck.Add(da);
        }
    }
    public void DrawCards()
    {
        if(cardsAmount > 6)
        {
            if(cards.Count < 6)
            {
                ReplenishCards();
            }
            StartCoroutine(DrawingCards());
        }
        else
        {
            if (cards.Count < 6)
            {
                ReplenishCards();
            }
            StartCoroutine(DrawingCards2());
        }
    }

    IEnumerator DrawingCards()
    {
        draggable = false;
        for (int a = 0; a < 6; a++)
        {
            InstantiateGameObject(cards[0]);
            discards.Add(cards[0]);
            cards.RemoveAt(0);

            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(0.5f);
        if (battleSystem.enemyCharacters.Count == 0 || battleSystem.playerCharacters.Count == 0)
        {
            endTurnButton.SetActive(false);
            draggable = false;
        }
        else
        {
            endTurnButton.SetActive(true);
            draggable = true;
        }
    }
    IEnumerator DrawingCards2()
    {
        draggable = false;
        for (int a = 0; a < 6; a++)
        {
            InstantiateGameObject(cards[0]);
            discards.Add(cards[0]);
            cards.RemoveAt(0);
            /*
             if (a == 5)
             {
                 discards.Add(cards[0]);
                 cards.RemoveAt(0);
             }*/
            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(0.5f);
        endTurnButton.SetActive(true);
        draggable = true;
    }
    private IEnumerator WaitAndDisappear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        text.text = "Your Turn!";
    }

    public void RemoveCardsFromHand()
    {
        foreach (GameObject cardsInHand in daCards)
        {
            cardsInHand.gameObject.transform.SetParent(null);
            Destroy(cardsInHand);
            slotNumber = 0;
        }
    }

    public void ReplenishCards()
    {
        discardsAmount = discards.Count;
        discards.Shuffle();
        for (int a = 0; a < discardsAmount; a++)
        {
            cards.Add(discards[0]);
            discards.RemoveAt(0);
        }
            discards.Clear();
    }

    public void AppearText(string textString, float time)
    {
        text.text = textString;
        text.gameObject.SetActive(true);
        StartCoroutine(WaitAndDisappear(time));
    }

    void InstantiateGameObject(CardAsset c)
    {
        if (c.HowManyTargets == NumberOfTargets.Single)
        {
            singleTargetPrefab.GetComponent<OneCardManager>().cardAsset = c;
            GameObject inHand = Instantiate(singleTargetPrefab, transform.position, Quaternion.identity) as GameObject;
            SetSpace(inHand);
            
        }
        else if (c.HowManyTargets == NumberOfTargets.Multiple)
        {
            multipleTargetPrefab.GetComponent<OneCardManager>().cardAsset = c;
            GameObject inHand = Instantiate(multipleTargetPrefab, transform.position, Quaternion.identity) as GameObject;
            SetSpace(inHand);
        }
    }

    void SetSpace(GameObject go)
    {
        go.transform.SetParent(slots.Children[slotNumber].transform);
        go.transform.position = slots.Children[slotNumber].transform.position;
        slotNumber++;
    }

}


