using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverSceneController : MonoBehaviour
{
    public Text won_text;
    private void Start()
    {

        ManageSound("GameOver");
        won_text.text = PlayerPrefs.GetString("WinnerName");
    }
    public void OnGameOverUIClicked(string name)
    {
        switch(name)
        {
            case "Replay":
                SceneManager.LoadScene("Game");
                break;
            case "BackToMenu":
                SceneManager.LoadScene("Menu");
                break;
        }
    }
    
}
