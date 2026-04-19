using UnityEngine;
using System.Collections.Generic;

public class AIStarter : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform AItargets;
    private List<string> targets;

    void Start()
    {
        CreateTargets("OneBlockSquareCW", -20f, 0f, 20f, "Enemy1");
        GameObject.Find("Enemy1").GetComponent<EnemyAI>().setLocations(targets.ToArray());
        CreateTargets("XLengthPatrol", 100f, 0f, 100f, "Enemy2");
        GameObject.Find("Enemy2").GetComponent<EnemyAI>().setLocations(targets.ToArray());
        CreateTargets("OneBlockSquareCW", -100f, 0f, -20f, "Enemy3");
        GameObject.Find("Enemy3").GetComponent<EnemyAI>().setLocations(targets.ToArray());
        CreateTargets("TwoBlockSquareCCW", 100f, 0f, -60f, "Enemy4");
        GameObject.Find("Enemy4").GetComponent<EnemyAI>().setLocations(targets.ToArray());
        CreateTargets("TwoBlockSquareCCW", -20f, 0f, -100f, "Enemy5");
        GameObject.Find("Enemy5").GetComponent<EnemyAI>().setLocations(targets.ToArray());
    }

    private void CreateTargets(string type, float xstart, float ystart, float zstart, string enemy)
    {
        targets = new List<string> { };
        Vector3 enemyPosition = new Vector3(xstart, ystart, zstart);
        GameObject enemyclone = Instantiate(enemyPrefab, enemyPosition, enemyPrefab.transform.rotation);
        enemyclone.name = enemy;
        if (type == "OneBlockSquareCW"){
            float xchange = 0f;
            float zchange = 0f;
            for (int i = 0; i < 4; i++){
                if (i == 1) {
                    xchange = -40f;
                } else if (i == 2) {
                    zchange = 40f;
                } else if (i == 3) {
                    xchange = 0f;
                }
                GameObject prefab = targetPrefab;
                Vector3 position = new Vector3( xchange + xstart, ystart, zchange + zstart);
                GameObject clone = Instantiate(prefab, position, prefab.transform.rotation);
                clone.name = enemy + prefab.name + (i + 1);
                targets.Add(clone.name);
                Debug.Log(targets[i]);
                clone.transform.SetParent(AItargets);
            }

        }
        if (type == "TwoBlockSquareCCW")
        {
            float xchange = 0f;
            float zchange = 0f;
            for (int i = 0; i < 4; i++)
            {
                if (i == 1)
                {
                    zchange = 80f;
                }
                else if (i == 2)
                {
                    xchange = -80f;
                }
                else if (i == 3)
                {
                    zchange = 0f;
                }
                GameObject prefab = targetPrefab;
                Vector3 position = new Vector3(xchange + xstart, ystart, zchange + zstart);
                GameObject clone = Instantiate(prefab, position, prefab.transform.rotation);
                clone.name = enemy + prefab.name + (i + 1);
                targets.Add(clone.name);
                Debug.Log(targets[i]);
                clone.transform.SetParent(AItargets);
            }

        }
        if (type == "XLengthPatrol")
        {
            float xchange = 0f;
            for (int i = 0; i < 4; i++)
            {
                if (i == 1){
                    xchange = -120f;
                }
                if (i == 2)
                {
                    xchange = -240f;
                }
                if (i == 3)
                {
                    xchange = -120f;
                }
                GameObject prefab = targetPrefab;
                Vector3 position = new Vector3(xchange + xstart, ystart, zstart);
                GameObject clone = Instantiate(prefab, position, prefab.transform.rotation);
                clone.name = enemy + prefab.name + (i + 1);
                targets.Add(clone.name);
                Debug.Log(targets[i]);
                clone.transform.SetParent(AItargets);
            }

        }



    }
}
