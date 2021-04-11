using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameObject PlayerArea;
    public GameObject CardBase;

    int n = 0;

    public List<CardAsset> deck = new List<CardAsset>();
    List<GameObject> CardsInHand = new List<GameObject>();
    /*create any amount of cards
     * add them to the list
     * then draw from them
     */
    void Awake()
    {
        deck.Shuffle();
    }

    public void OnClick()
    {
        int x = -5;
        Vector3 handPosition = new Vector3(x, -3, 0);


        for ( int i = 0; i < deck.Count; i++)
        {
            //creates a random deck of cards up to 5 aka count
            //CardAsset playerCard = Instantiate(deck[Random.Range(0, deck.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            //angle += 18;
            CardBase.GetComponent<OneCardManager>().cardAsset = deck[n];
            CardBase.transform.SetParent(PlayerArea.transform, true);
            GameObject playerCard = Instantiate(CardBase, handPosition, Quaternion.identity);

            x += 2;
            handPosition.x = x;
            n++;
        }
    }



}
