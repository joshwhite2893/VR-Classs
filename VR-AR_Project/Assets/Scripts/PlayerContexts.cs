using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContexts : MonoBehaviour
{
    public PlayerController playerCont;
    public GManager gManage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bound1"))
        {
            gManage.PlayerOutOfBounds(1);
        }
        else if (other.gameObject.CompareTag("Bound2"))
        {
            gManage.PlayerOutOfBounds(2);

        } else if (other.gameObject.CompareTag("Cannon"))
        {
            gManage.CannonChange(1);
        } else if (other.gameObject.CompareTag("Ending"))
        {
            gManage.ReachedFinish();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cannon"))
        {
            gManage.CannonChange(0);
        } else if (other.gameObject.CompareTag("Gate1"))
        {
            gManage.Platform1To2Lights();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Combo"))
        {
            gManage.ComboStart();
        }
    }
}
