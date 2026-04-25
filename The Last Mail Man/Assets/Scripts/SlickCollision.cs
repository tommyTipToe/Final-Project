using UnityEngine;

public class SlickCollision : MonoBehaviour
{
    private bool isOnOil = false;
    private bool slow = false;
    private bool fast = false;
    private EnemyAI enemyAI;
    private CarControls carControls;



    void OnTriggerEnter(Collider collider)
    {
        isOnOil = true;

        if (collider.transform.root.CompareTag("Player")){
            fast = true;
            carControls = collider.GetComponentInParent<CarControls>();
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
        fast = false;
        if (carControls)
        {
            Invoke(nameof(clearBoost), 10f);
        }
        if (enemyAI){
            enemyAI.setSlowed(false);
        }
    }

    void Update()
    {
        if (isOnOil)
        {
            if (slow)
            {
                enemyAI.setSlowed(true);
            }
            if (fast)
            {
                carControls.maxSpeed = 30f;
                carControls.boost = true;
            }
        }
    }

    void clearBoost()
    {
        carControls.maxSpeed = 20f;
        carControls.boost = false;
    }
}
