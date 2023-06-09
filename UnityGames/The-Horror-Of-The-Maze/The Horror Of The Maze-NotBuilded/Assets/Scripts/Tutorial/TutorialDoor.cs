using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoor : MonoBehaviour
{
    public Animator animator;
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
    }

    public void doDoorOpen()
    {
        animator.SetBool("Open", true);

    }
    public void doDoorClose()
    {
        animator.SetBool("Open", false);

    }

    void Update()
    {
        if ((Player.transform.position - this.transform.position).sqrMagnitude < 1 * 1)
        {
            
            if (animator.GetBool("Open") == false && Input.GetKeyDown(KeyCode.E))
            {
                doDoorOpen();
            }
            else if (animator.GetBool("Open") == true && Input.GetKeyDown(KeyCode.E))
            {
                doDoorClose();
            }
        }
    }
}
