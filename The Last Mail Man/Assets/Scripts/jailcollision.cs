using UnityEngine;

public class jailcollision : MonoBehaviour
{

    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.root.CompareTag("Player"))
        {
            Debug.Log("pickup");
            if (player.GetComponent<CarControls>().jailPoints > 0)
            {
                player.GetComponent<CarControls>().changeTransitScore(player.GetComponent<CarControls>().jailPoints);
                player.GetComponent<CarControls>().changeJailScore(0 - player.GetComponent<CarControls>().jailPoints);
            }

        }

    }
}