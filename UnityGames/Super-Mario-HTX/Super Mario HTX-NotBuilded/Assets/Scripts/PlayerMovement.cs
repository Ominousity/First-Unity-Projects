using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public float speed;
    public float Jumpforce;
    private float moveinput;

    private Rigidbody2D rb;
    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    public GameObject coinScore; //UI Text Object
    public GameObject powerFlame; //UI Text Object

    private float horizontalMove = 0f;
    private bool jump = false;

    private bool powerUp = false;
    private int powerTimer = 5; //Seconds Overall

    private int coin = 0;
    public int totalCoin = 0;

    public AudioSource enemySound;
    public AudioSource bossSound;
    public AudioSource mikeSound;

    private void Start()
    {
        var aSources = GetComponents<AudioSource>();
        enemySound = aSources[0];
        bossSound = aSources[1];
        mikeSound = aSources[2];
        powerFlame.SetActive(false);
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.gameObject.transform.position.y <= -10)
        {
            mikeSound.Play(0);
            SceneManager.LoadScene("Death");
        }
        print(powerUp);

        if (powerTimer <= 0)
        {
            powerUp = false;
            powerFlame.SetActive(false);
        }
        //animation transition
        animator.SetFloat("Speed", Mathf.Abs(moveinput));

        // reset amount of jumps when grounded
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        // get input to jump
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * Jumpforce;
            extraJumps = extraJumpsValue - 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * Jumpforce;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //Get input for movement and move

        moveinput = Input.GetAxis("Horizontal");
        Debug.Log(moveinput);
        rb.velocity = new Vector2(moveinput * speed, rb.velocity.y);

        if (facingRight == false && moveinput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveinput < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var o = collision.gameObject;
        if (o.CompareTag("enemy"))
        {
            if (!powerUp)
            {
                if (gameObject.transform.position.y >= o.transform.position.y)
                {
                    enemySound.Play(0);

                    Destroy(o);
                }
                else
                {
                    mikeSound.Play(0);
                    Destroy(this.gameObject);
                    SceneManager.LoadScene("Death");
                }
            }
            else
            {
                enemySound.Play(0);
                Destroy(o);
            }
        }

        if (o.CompareTag("powerUp"))
        {
            Destroy(o);
            powerUp = true;
            powerFlame.SetActive(true);
            StartCoroutine("LoseTime");
        }

        if (o.CompareTag("coin"))
        {
            Destroy(o);
            coin++;
            coinScore.GetComponent<UnityEngine.UI.Text>().text = ("Coins: " + coin + "/" + totalCoin);
        }

        if (o.CompareTag("doorToLevel02"))
        {
            SceneManager.LoadScene("Level02");
        }

        if (o.CompareTag("doorToLevel03"))
        {
            SceneManager.LoadScene("Level03");
        }
    }

    private IEnumerator LoseTime()
    {
        while (powerUp)
        {
            yield return new WaitForSeconds(1);
            powerTimer--;
        }
    }
}