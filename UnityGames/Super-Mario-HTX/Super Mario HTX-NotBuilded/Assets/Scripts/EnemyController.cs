using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    public float speed = 1;

    private bool moveRight = false;

    // Start is called before the first frame update
    private void Start()
    {
        Flip();
    }

    // Update is called once per frame
    private void Update()
    {
        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("turns"))
        {
            if (!moveRight)
            {
                Flip();
                moveRight = true;
            }
            else
            {
                Flip();
                moveRight = false;
            }
        }
    }

    private void Flip()
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}