using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour{
    enum State { WANDERING, CHASING }

    [SerializeField] private GameObject[] locations;

    private NavMeshAgent nav;
    private GameObject player;
    private int currentLocation = 0;
    private State state = State.WANDERING;
    private float speed = 7.0f;
    private float fast = 7.0f;
    private float slow = 3.5f;
    private float sightRange = 15f;
    private float fieldOfView = 90f;


    void Start(){
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("ExamplePlayer");
        locations = new GameObject[]{
            GameObject.Find("Target1"),
            GameObject.Find("Target2"),
        };
        nav.SetDestination(locations[0].transform.position);
    }


    void Update(){
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