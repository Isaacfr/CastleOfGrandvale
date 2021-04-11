using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
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

    public void NextLevel()
    {
        SceneManager.LoadScene("CardSelectionScene");
    }

    public void Level85()
    {

        SceneManager.LoadScene("Scene9.1");
    }
}
