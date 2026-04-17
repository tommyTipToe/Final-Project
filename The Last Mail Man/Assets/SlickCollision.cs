using UnityEngine;

public class SlickCollision : MonoBehaviour
{
    private bool isOnOil = false;
    private bool slow = false;
    private EnemyAI enemyAI; 



    void OnTriggerEnter(Collider collider)
    {
        isOnOil = true;

        if (collider.gameObject.tag == "Player"){

        }
        if (collider.gameObject.tag == "Enemy")
        {
            enemyAI = collider.gameObject.GetComponent<EnemyAI>();
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
