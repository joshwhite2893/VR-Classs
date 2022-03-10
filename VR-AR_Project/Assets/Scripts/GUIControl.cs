using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControl : MonoBehaviour
{
    public GameObject rootPlatform;
    public GameObject startMenu;
    public GameObject endMenu;
    public GameObject gameGUI;
    public GameObject hurtScreen;
    public Text healthVal;
    public Text timeVal;
    public Text timeReport;
    public GameObject goalText;
    public GameObject failText;
    public GameObject cannonButton;
    

    private GManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = rootPlatform.GetComponent<GManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonPressed()
    {
        gameManager.StartGame();
    }

    public void RestartButtonPressed()
    {
        gameManager.RestartGame();
    }

    public void QuitButtonPressed()
    {
        gameManager.QuitGame();
    }

    // Helper funcs. that are call & forget.
    // Implementer worries about activation of different panels - not client.
    public void EnableStartMenu()
    {
        startMenu.SetActive(true);
        endMenu.SetActive(false);
        gameGUI.SetActive(false);
    }

    public void EnableEndMenu()
    {
        gameGUI.SetActive(false);
        startMenu.SetActive(false);
        endMenu.SetActive(true);
    }

    public void DisableMenusForGame()
    {
        gameGUI.SetActive(true);
        startMenu.SetActive(false);
        endMenu.SetActive(false);
    }

    public void EndMenuTextSet(int option)
    {
        //option is either 0 or 1. 0 if the user failed, 1 if the user reached the finish
        if(option == 0)
        {
            goalText.SetActive(false);
            failText.SetActive(true);
        } else if(option == 1)
        {
            failText.SetActive(false);
            goalText.SetActive(true);
        }
    }

    public void SetTimeValue(int newTime)
    {
        string strTime = newTime.ToString();
        timeVal.text = strTime;
    }

    public void SetHealthValue(int newHealth)
    {
        string strHealth = newHealth.ToString();
        healthVal.text = strHealth;
    }

    public IEnumerator FlashHurt()
    {
        hurtScreen.SetActive(true);
        yield return new WaitForSeconds(.25f);
        hurtScreen.SetActive(false);
        yield break;
    }

    public void CannonButtonChange(int change)
    {
        if(change == 1)
        {
            cannonButton.SetActive(true);
        } else
        {
            cannonButton.SetActive(false);
        }
    }

    public void ReportTime(int reportedTime)
    {
        string newText = "Time: " + reportedTime.ToString();
        timeReport.text = newText;
    }
}
