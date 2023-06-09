using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public TextMeshProUGUI FlashlightText;
    // Start is called before the first frame update
    void Start()
    {
        FlashlightText.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        other.gameObject.CompareTag("Player");
        FlashlightText.enabled = true;
    }
    void OnTriggerExit(Collider other)
    {
        other.gameObject.CompareTag("Player");
        FlashlightText.enabled = false;
    }
}
