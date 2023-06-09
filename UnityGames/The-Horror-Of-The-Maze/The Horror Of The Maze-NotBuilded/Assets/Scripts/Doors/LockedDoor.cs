using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Locked_Door;
    GameObject Player;
    private float timerTime = 2.0f;
    private bool buttonPressed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Locked_Door.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.transform.position - this.transform.position).sqrMagnitude < 1 * 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                buttonPressed = true;
                Locked_Door.enabled = true;
                
            }
        }
        if (buttonPressed == true)
        {
            timerStarted();
        }
        if (timerTime <= 0.0f)
        {
            timerEnded();
        }
    }

 

    void timerStarted()
    {
        timerTime -= Time.deltaTime;
    }

    void timerEnded()
    {
        Locked_Door.enabled = false;
        buttonPressed = false;
        timerTime = 2.0f;
    }
}
