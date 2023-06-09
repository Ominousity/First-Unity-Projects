using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedDoor: MonoBehaviour
{
    public Animator animator;
    GameObject Player;
    public Text DoorPressE;
    string doorPressE = "Press E to Use Door...";

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        DoorPressE.enabled = false;
        animator = GetComponent<Animator>();
    }
    
    public void doDoorOpen()
    {
        animator.SetBool("Open", true);

    }
    public void doDoorClose()
    {
        animator.SetBool("Open", false);

    }

    void OnTriggerStay(Collider other)
    {
        other.gameObject.CompareTag("Player");
        DoorPressE.enabled = true;
    }


    void OnTriggerExit(Collider other)
    {
        other.gameObject.CompareTag("Player");
        DoorPressE.enabled = false;
    }

    void Update()
    {
        if ((Player.transform.position - this.transform.position).sqrMagnitude < 2 * 2)
        {
            //DoorPressE.enabled = true;
            if (animator.GetBool("Open") == false && Input.GetKeyDown(KeyCode.E))
            {
                doDoorOpen();
            }
            else if (animator.GetBool("Open") == true && Input.GetKeyDown(KeyCode.E))
            {
                doDoorClose();
            }
            if ((Player.transform.position - this.transform.position).sqrMagnitude < 1 * 1)
            {
                //DoorPressE.enabled = false;
                
            }
        }
    }
}
