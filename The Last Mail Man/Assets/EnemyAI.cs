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
    private bool locked = false;
    private float speed = 7.0f;


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
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < 15f){
            StartChasing();
        }
        else if (distance >= 15f){
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
        SetState(State.CHASING);
    }


    void StartWandering(){
        SetState(State.WANDERING);
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
        if (!locked) {
            speed = speed / 2;
            locked = true;
        }
        Debug.Log(speed);

    }

    public void notSlowed(){
        speed = speed * 2;
        locked = false;
        Debug.Log(speed);
    }
}