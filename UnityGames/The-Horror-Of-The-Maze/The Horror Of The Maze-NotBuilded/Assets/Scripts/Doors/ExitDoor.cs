using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    private static int Score;
    public GameObject Spotlight;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Spotlight.SetActive(false);
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Score = PlayerManager.inventory;

        if (Score == 9)
        {
            Spotlight.SetActive(true);

            if ((Player.transform.position - this.transform.position).sqrMagnitude < 1 * 1)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(3);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }
}
