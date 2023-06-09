using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialObject : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.transform.position - this.transform.position).sqrMagnitude < 2 * 2)
        {
            print("enabled");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject);
                PlayerManager.AddToInventory();
            }

        }
    }
}
