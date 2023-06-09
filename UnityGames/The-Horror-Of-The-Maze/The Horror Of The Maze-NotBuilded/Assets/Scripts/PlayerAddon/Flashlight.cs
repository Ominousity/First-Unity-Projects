using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool flashLightEnabled;
    public GameObject flashLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //hvis keypress F sker, virker child spotlight lys og bliver enabled i spillet
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashLightEnabled = !flashLightEnabled;
        }
        if (flashLightEnabled)
        {
            flashLight.SetActive(true);
        }
        else
        {
            flashLight.SetActive(false);
        }
    }
}
