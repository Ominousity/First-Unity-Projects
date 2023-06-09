using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialNoteScript : MonoBehaviour
{
    public TextMeshProUGUI NoteText;
    // Start is called before the first frame update
    void Start()
    {
        NoteText.enabled = false;
    }
    void OnTriggerStay(Collider other)
    {
        other.gameObject.CompareTag("Player");
        NoteText.enabled = true;
    }
    void OnTriggerExit(Collider other)
    {
        other.gameObject.CompareTag("Player");
        NoteText.enabled = false;
    }
}
