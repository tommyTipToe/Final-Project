using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


public class EnemyAI : MonoBehaviour
{
    enum State { WANDERING, CHASING, RETREATING}

   

    private GameObject[] locations;

    private NavMeshAgent nav;
    private GameObject player;
    private int currentLocation = 0;
    private State state = State.WANDERING;
    private float speed = 7f;
    private float fast = 7f;
    private float slow = 3.5f;
    private float fastChasing = 12f;
    private float slowChasing = 6f;
    private float sightRange = 30f;
    private float fieldOfView = 90f;
    private bool hardmode;
    private bool slowed = false;
    private Transform jail;

    public void setLocations(string[] newLocaitons)
    {
        List<GameObject> tempLocations = new List<GameObject> { };
        for (int i = 0; i < newLocaitons.Length; i++)
        {
            tempLocations.Add(GameObject.Find(newLocaitons[i]));
        }
        locations = tempLocations.ToArray();
        nav.SetDestination(locations[0].transform.position);
    }

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        jail = GameObject.Find("JailDropOff").transform;
    }
    void Start()
    {
        hardmode = GameObject.Find("Difficulty").GetComponent<difficultyManager>().getDifficulty();
        if (hardmode)
        {
            speed = 8f;
            fastChasing = 15f;
            slowChasing = 7f;
            fast = 8f;
            slow = 4f;
            sightRange = 50f;
            fieldOfView = 120f;
        }
        player = GameObject.FindWithTag("Player");


    }


    void Update()
    {
        if(state == State.RETREATING)
        {
            if (nav.remainingDistance < 0.6f && nav.remainingDistance > 0f)
            {
                Debug.Log("Stole Package");
                StartWandering();
            }
            else
            {
                return;
            }
            
        }
        if (locations == null)
        {
            return;
        }
        checkSpeed();



        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        Debug.DrawRay(transform.position, directionToPlayer * sightRange, Color.red);
        if (angle < fieldOfView / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightRange))
            {
                if (hit.transform.root == player.transform)
                {
                    StartChasing();
                }
                else
                {
                    StartWandering();
                }
            }
            else
            {
                StartWandering();
            }

        }
        else
        {
            StartWandering();
        }


        if (state == State.WANDERING && nav.remainingDistance < 0.6f)
        {
            nav.SetDestination(locations[++currentLocation % locations.Length].transform.position);
        }
        if (state == State.CHASING)
        {
            UpdateChasing();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.CompareTag("Player") && state != State.RETREATING){
            Debug.Log("caught");
            StartRetreating();
            
        }
        
    }


    void StartChasing()
    {
        if (state is State.CHASING) return;
        fieldOfView = 360f;
        SetState(State.CHASING);
    }


    void StartWandering()
    {
        SetState(State.WANDERING);
        fieldOfView = 90f;
        nav.speed = speed;
        nav.SetDestination(locations[currentLocation % locations.Length].transform.position);
    }

    void StartRetreating() {
        SetState(State.RETREATING);
        nav.speed = 12f;
        nav.SetDestination(jail.position);
        Debug.Log("Retreating");
    }


    void UpdateChasing()
    {
        nav.SetDestination(player.transform.position);
        nav.speed = speed;

    }

    void SetState(State newState)
    {
        state = newState;
        //Debug.Log(state);
    }

    public void setSlowed(bool slow)
    {
        slowed = slow;
    }

    void checkSpeed()
    {
        if (state == State.CHASING && slowed == false)
        {
            speed = fastChasing;
        }
        else if (state == State.CHASING && slowed == true)
        {
            speed = slowChasing;
        }
        else if (state == State.WANDERING && slowed == false)
        {
            speed = fast;
        }
        else if (state == State.WANDERING && slowed == true)
        {
            speed = slow;
        }

        
    }
}