using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public GUIControl gui;
    public PlayerController player;
    public float Health { get; set; }
    float Time { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Health = 100f;
        Time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gui.DisableMenus();
        player.isBall = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        // reinitialize values & disable menus
    }
}
