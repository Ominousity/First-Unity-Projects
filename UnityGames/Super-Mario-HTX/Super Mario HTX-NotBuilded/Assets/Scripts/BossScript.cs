using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public int health = 5;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var o = collision.gameObject;
        if (o.CompareTag("Player"))
        {
            if (o.transform.position.y >= this.transform.position.y)
            {
                health--;
                if (health <= 0)
                {
                    Destroy(this.gameObject);
                    SceneManager.LoadScene("Win");
                }
                o.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 500));
            }
            else
            {
                Destroy(o.gameObject);
                SceneManager.LoadScene("Death");
            }
        }
    }
}