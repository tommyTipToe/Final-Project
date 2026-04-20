using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


public class EnemyAI : MonoBehaviour{
    enum State { WANDERING, CHASING }

    private GameObject[] locations;

    private NavMeshAgent nav;
    private GameObject player;
    private int currentLocation = 0;
    private State state = State.WANDERING;
    private float speed = 7f;
    private float fast = 7f;
    private float slow = 3.5f;
    private float sightRange = 15f;
    private float fieldOfView = 90f;
    private bool hardmode;

    public void setLocations(string[] newLocaitons){
        List<GameObject> tempLocations = new List<GameObject> { };
        for (int i = 0; i < newLocaitons.Length; i++){
            tempLocations.Add(GameObject.Find(newLocaitons[i]));
        }
        locations = tempLocations.ToArray();
        nav.SetDestination(locations[0].transform.position);
    }

    void Awake(){
        nav = GetComponent<NavMeshAgent>();
    }
    void Start(){
        hardmode = GameObject.Find("Difficulty").GetComponent<difficultyManager>().getDifficulty();
        if (hardmode){
            speed = 9f;
            fast = 9f;
            slow = 4.5f;
            sightRange = 25f;
            fieldOfView = 120f;
        }
        player = GameObject.Find("ExamplePlayer");
        
    }


    void Update(){
        if (locations == null){
            return;
        }
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle < fieldOfView / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightRange))
            {
                if (hit.transform == player.transform)
                {
                    StartChasing();
                }


            }else{
                StartWandering();
            }
        }else{
            StartWandering();
        }

        if (state == State.WANDERING && nav.remainingDistance < 0.6f){
            nav.SetDestination(locations[++currentLocation % locations.Length].transform.position);
        }
        if (state == State.CHASING){
            UpdateChasing();
        }
    }



    void StartChasing(){
        if (state is State.CHASING) return;
        fieldOfView = 360f;
        SetState(State.CHASING);
    }


    void StartWandering(){
        SetState(State.WANDERING);
        fieldOfView = 90f;
        nav.speed = speed;
        nav.SetDestination(locations[currentLocation % locations.Length].transform.position);
    }


    void UpdateChasing(){
        nav.SetDestination(player.transform.position);
        nav.speed = speed;
    }

    void SetState(State newState){
        state = newState;
        //Debug.Log(state);
    }

    public void slowed(){
        speed = slow;
    }

    public void notSlowed(){
        speed = fast;
    }
}