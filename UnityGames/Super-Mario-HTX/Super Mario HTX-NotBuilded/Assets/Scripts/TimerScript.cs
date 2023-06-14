using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class TimerScript : MonoBehaviour
{
    public int timeLeft = 60; //Seconds Overall
    public GameObject countdown; //UI Text Object

    private void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }

    private void Update()
    {
        countdown.GetComponent<UnityEngine.UI.Text>().text = ("Time: " + timeLeft); //Showing the Score on the Canvas
        print(timeLeft);
        if (timeLeft <= 0)
        {
            SceneManager.LoadScene("Death");
        }
    }

    private IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}