using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialDoorScript : MonoBehaviour
{
    public TextMeshProUGUI DoorText;
    // Start is called before the first frame update
    void Start()
    {
        DoorText.enabled = false;
    }
    void OnTriggerStay(Collider other)
    {
        other.gameObject.CompareTag("Player");
        DoorText.enabled = true;
    }
    void OnTriggerExit(Collider other)
    {
        other.gameObject.CompareTag("Player");
        DoorText.enabled = false;
    }
}
