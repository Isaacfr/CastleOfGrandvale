using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    // Start is called before the first frame update
    CardAsset thisCard;
    CardSelection cardSelection;

    private void Start()
    {
        cardSelection = FindObjectOfType<CardSelection>();
        thisCard = GetComponent<OneCardManager>().cardAsset;
        if(thisCard == null)
        {
            Debug.Log("none");
        }
    }
    void OnMouseDown()
    {
        KeepingSameDeck.AddNewCards(thisCard);
        Destroy(this.gameObject);
        cardSelection.DestroyOtherCards();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
