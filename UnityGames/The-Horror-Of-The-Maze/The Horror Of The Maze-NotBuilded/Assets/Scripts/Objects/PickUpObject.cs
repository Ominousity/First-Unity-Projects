

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : MonoBehaviour
{
    //GameObject Inventory;
    GameObject Player;
    Text PressEText;
    string pressEText = "Press E to pick up note...";
    // Start is called before the first frame update
    void Start()
    {
        //find tag af spiller, disable UI teksten således det ikke kommer i starten
       // Inventory = GameObject.FindWithTag("Inventory");
        Player = GameObject.FindWithTag("Player");
        PressEText = GameObject.Find("NotePressE").GetComponent<Text>();
        PressEText.enabled = false;
    }
    //når spilleren når inde i collideren, kommer UI tekst frem
    void OnTriggerStay(Collider other)
    {
        PressEText.enabled = true;
    }

    //når spilleren går ud af collideren, kommer UI tekst frem

    void OnTriggerExit(Collider other)
    {
        PressEText.enabled = false;
    }

    void Update()
    {
        if ((Player.transform.position - this.transform.position).sqrMagnitude < 2 * 2)
        {
            //Ødelægger noterne når key press down E
            print("enabled");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject);
                PlayerManager.AddToInventory();
                PressEText.enabled = false;
            }

        }
       



    }




}
