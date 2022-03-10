using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public GUIControl gui;
    public PlayerController player;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject light1;
    public GameObject light2;

    public int Health { get; set; }
    public int Time { get; set; }
    private bool gameActive;
    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        Time = 0;
        gameActive = false;
    }

    public void StartGame()
    {
        gui.DisableMenusForGame();
        player.isBall = true;
        gameActive = true;
        StartCoroutine(TimerOn());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        // reinitialize values & disable menus
        Time = 0;
        Health = 100;
        gui.SetTimeValue(0);
        gui.SetHealthValue(100);
        light1.SetActive(true);
        light2.SetActive(false);
        //call reset to player as well, reset bools, reset positions, unparent sphere, etc
        player.RestartGame();
        StartGame();
    }

    private IEnumerator TimerOn()
    {
        while (gameActive)
        {
            yield return new WaitForSecondsRealtime(1f);
            Time += 1;
            gui.SetTimeValue(Time);
        }

        yield return null;
        
    }

    public void PlayerOutOfBounds(int boundHit)
    {
        // boundhit can be 1 or 2, signaling which bound was hit - this will help determine respawn
        Health -= 10;
        gui.StartCoroutine(gui.FlashHurt());

        if(Health > 0)
        {
            gui.SetHealthValue(Health);
            // want to cancel all forces on the sphere so that it doesn't maintain inertia after respawn
            player.sphereRigid.velocity = Vector3.zero;
            player.sphereRigid.angularVelocity = Vector3.zero;

            if (boundHit == 1)
            {
                player.playSphere.transform.position = spawn1.transform.position;
            } else
            {
                player.playSphere.transform.position = spawn2.transform.position;
            }
        } else
        {
            gui.EndMenuTextSet(0);
            gui.EnableEndMenu();
        }

    }

    public void CannonChange(int onOff)
    {
        //onOff is passed as either a 1 - if the player entered cannon. 0 - if the player left cannon.
        gui.CannonButtonChange(onOff);
    }

    public void Platform1To2Lights()
    {
        light1.SetActive(false);
        light2.SetActive(true);
    }

    public void ComboStart()
    {
        player.CombinePlayer();    
    }

    public void ReachedFinish()
    {
        gameActive = false;
        player.isBall = false;
        player.isCombo = false;
        gui.EndMenuTextSet(1);
        gui.ReportTime(Time);
        gui.EnableEndMenu();
    }
}
