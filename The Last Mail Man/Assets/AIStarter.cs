using UnityEngine;
using System.Collections.Generic;

public class AIStarter : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform AItargets;
    private List<string> targets = new List<string> { };

    void Start()
    {
        CreateTargets("OneBlockSquare", -20f, 0f, 20f, "Enemy1");
        GameObject.Find("Enemy1").GetComponent<EnemyAI>().setLocations(targets.ToArray());
    }

    private void CreateTargets(string type, float xstart, float ystart, float zstart, string enemy)
    {
        Vector3 enemyPosition = new Vector3(xstart, ystart, zstart);
        GameObject enemyclone = Instantiate(enemyPrefab, enemyPosition, enemyPrefab.transform.rotation);
        enemyclone.name = enemy;
        if (type == "OneBlockSquare"){
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
                clone.transform.SetParent(AItargets);
            }

        }
        


    }
}
