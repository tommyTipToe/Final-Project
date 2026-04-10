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


    void OnCollisionStay(Collision collision)
    {
        isOnOil = true;

        if (collision.gameObject.tag == "Player"){

        }
        if (collision.gameObject.tag == "Enemy")
        {
            slow = true;
        }
    }

    void OnCollisionExit(Collision collision)
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
