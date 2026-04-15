using UnityEngine;

public class SlickCollision : MonoBehaviour
{
    private bool isOnOil = false;
    private bool slow = false;
    private GameObject enemy;
    private EnemyAI enemyAI; 


    void Start()
    {
        enemy = GameObject.Find("Enemy");
        enemyAI = enemy.GetComponent<EnemyAI>();
    }


    void OnTriggerEnter(Collider collider)
    {
        isOnOil = true;

        if (collider.gameObject.tag == "Player"){

        }
        if (collider.gameObject.tag == "Enemy")
        {
            slow = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        isOnOil = false;
        slow = false;
        enemyAI.notSlowed();
    }

    void Update()
    {
        if (isOnOil)
        {
            if (slow)
            {
                enemyAI.slowed();
            }
        }
    }
}
