using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class KeepingSameDeck
{
    public static List<CardAsset> sameDeck = new List<CardAsset>();

    public static void InitializeStartingDeck(List<CardAsset> cardsOfDeck)
    {
        sameDeck = cardsOfDeck;
    }

    public static void SummonSameDeck(List<CardAsset> cardsOfDeck)
    {
        cardsOfDeck = sameDeck;
    }

    public static void AddNewCards(CardAsset ca)
    {
        sameDeck.Add(ca);
    }

}
