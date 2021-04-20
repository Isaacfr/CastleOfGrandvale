using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    Deck deck;
    private void Start()
    {
        deck = FindObjectOfType<Deck>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ReturnToTitle()
    {
        KeepingSameDeck.sceneNumber = 0;
        SceneManager.LoadScene("TitleScene");
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void CardSelectionTutorialScene()
    {
        SceneManager.LoadScene("TutorialCardSelectionScene");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Scene9");
    }
    public void NextLevel()
    {
        deck.EndButtonDeck();
        SceneManager.LoadScene("CardSelectionScene");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
    public void EndLevel()
    {
        SceneManager.LoadScene("EndScene");
    }
    public void LevelAfterCardSelect()
    {
        switch(KeepingSameDeck.sceneNumber)
        {
            case 1:
                SceneManager.LoadScene("Scene9.1");
                break;
            case 2:
                SceneManager.LoadScene("Scene9.3");
                break;
            case 3:
                SceneManager.LoadScene("Scene10.1");
                break;
            case 4:
                SceneManager.LoadScene("Scene10.2");
                break;
            case 5:
                SceneManager.LoadScene("Scene11.1");
                break;
            case 6:
                SceneManager.LoadScene("Scene11.2");
                break;
         
        }
    }
}
