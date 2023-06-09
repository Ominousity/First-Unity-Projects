using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;



public class EnemyController : MonoBehaviour
{
    //radius for monster går efter player
    public float lookRadius = 6f;
    //radius for monster lyd
    public float soundRadius = 8f;

    Transform target;
    NavMeshAgent agent;
    GameObject[] waypoints;
    GameObject nextWaypoint;
    int nextWaypointNum;
    Vector3 curPos;
    Vector3 lastPos;
    AudioSource monsterSound;
    bool destArrived = true;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        monsterSound = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("UnlockedDoor"))
        {
            if (other.gameObject.GetComponent<UnlockedDoor>().animator.GetBool("Open") == false)
            {
                print("open door");
                other.gameObject.GetComponent<UnlockedDoor>().doDoorOpen();
            }

        
        }
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    //Tjekker om spilleren bevæger sig med metoden isPlayerMoving
    //tjekker om spilleren er inde for "høreradiusen". Hvis den er inde for Target, så vil den løbe efter vedkommende hvis playeren laver lyd
    // Update is called once per frame
    void Update() {

        float distance = Vector3.Distance(target.position, transform.position);
        PlayMonsterSound(distance);

        if (distance <= lookRadius && IsPlayerMoving())
        {
            destArrived = true;
            agent.SetDestination(target.position);
            FaceTarget();

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }

        }
        else
        {
            WalkToWayPoint();
        }
            
        UpdateLookRadius();
        UpdateSpeed();
        

    }

    //checker om spiller er i radius for at monster lyden skal spilles
    void PlayMonsterSound(float distance)
    {
        if (distance <= soundRadius) {
            //play sound hvis lyden ikke allerede spiller
            if (!monsterSound.isPlaying)
            {
                monsterSound.Play();
            }
        }
        else
        {
            monsterSound.Stop();
        }
    }

    //checker om player bevære sig ved at tjekke gamle position er lige med nye position
    public bool IsPlayerMoving()
    {
        curPos = target.transform.position;
        if (curPos == lastPos)
        {
            return false;
        }
        lastPos = curPos;
        return true;
    }

    //får monsteren til at gå rundt til waypoint og tjekker om den har nået waypoint
    void WalkToWayPoint()
    {
        if (destArrived)
        {
            SetNextWaypoint();
            agent.SetDestination(nextWaypoint.transform.position);
            destArrived = false;
        }

        float distance  = Vector3.Distance(nextWaypoint.transform.position, agent.transform.position);
        if (distance < 5)
        {
            print("arrived");
            destArrived = true;

        }
    }

    //vælger en random waypoint 
    void SetNextWaypoint()
    {
        var rand= Random.Range(0, waypoints.Length - 1);
        if(rand == nextWaypointNum)
        {
            //do it again because you found the same endpoint
            SetNextWaypoint();
        }
        nextWaypoint = waypoints[nextWaypointNum];
        nextWaypointNum = rand;
    }

    Transform GetClosestEnemy()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in waypoints)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }

        return tMin;
    }
    //radiusen og speeden bliver større for hver note
    void UpdateLookRadius()
    {
        lookRadius = (2 * PlayerManager.GetInvetory()) + 6;
    }

    void UpdateSpeed()
    {
        agent.speed = PlayerManager.GetInvetory() + 4f;
    }

    //sørger for at fjenden vender den rigtige retning ift. spilleren
    void FaceTarget()
    {
          Vector3 direction = (target.position - transform.position).normalized;
          Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
          transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }

    //farver fjenden
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

  
}
