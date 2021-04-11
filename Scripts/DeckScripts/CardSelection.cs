using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SummonCards());
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
