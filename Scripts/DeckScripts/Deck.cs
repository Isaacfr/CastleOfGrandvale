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

    public int slotNumber = 0;

    [SerializeField]
    GameObject[] daCards;
    [SerializeField]
    bool draggable;

    public bool checkDeck;

    public GameObject endTurnButton;
    void Awake()
    {

        if (SceneManager.GetActiveScene().name == "Scene9")
        {
            foreach (CardAsset cA in cards)
            {
                KeepingSameDeck.sameDeck.Add(cA);
            }
        }
        else
        {
            cards = KeepingSameDeck.sameDeck;
        }
        cards.Shuffle();
        AppearText("Drawing cards...", 8.0f);

    }

    private void Start()
    {
        cardsAmount = cards.Count;
        endTurnButton.SetActive(false);
        Invoke("DrawCards", 2.0f);
    }

    private void Update()
    {
        Debug.Log("here " + KeepingSameDeck.sameDeck.Count);
        daCards = GameObject.FindGameObjectsWithTag("Cards");
        if (draggable == false)
        {
            foreach(GameObject daCard in daCards)
            {
                if(daCard.transform.GetComponent<BoxCollider>() == null)
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
        for (int a = 0; a < 6 ; a++)
        {
            InstantiateGameObject(cards[0]);
            discards.Add(cards[0]);
            cards.RemoveAt(0);

            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(0.5f);
        endTurnButton.SetActive(true);
        draggable = true;
    }
    IEnumerator DrawingCards2()
    {
        draggable = false;
        for (int a = 0; a < 6; a++)
        {
            InstantiateGameObject(cards[0]);
            discards.Add(cards[0]);
            cards.RemoveAt(0);

             if (a == 5)
             {
                 discards.Add(cards[0]);
                 cards.RemoveAt(0);
             }
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


