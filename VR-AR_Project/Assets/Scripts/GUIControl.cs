using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIControl : MonoBehaviour
{
    public GameObject rootPlatform;
    public GameObject startMenu;
    public GameObject endMenu;

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
    }

    public void EnableEndMenu()
    {
        startMenu.SetActive(false);
        endMenu.SetActive(true);
    }

    public void DisableMenus()
    {
        startMenu.SetActive(false);
        endMenu.SetActive(false);
    }

}
