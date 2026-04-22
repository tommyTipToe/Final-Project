using System.Collections.Generic;
using UnityEngine;

public class HousePlacer : MonoBehaviour{
    [SerializeField] private List<GameObject> housePrefabs;
    [SerializeField] private Transform houses;

    void Start(){
        CreateHouses();
    }

    private void CreateHouses() {
        for (int j = -3; j < 3; j++) {
            for (int i = 0; i < 4; i++) {
                GameObject prefab = housePrefabs[1];
                Vector3 position = new Vector3((i * (-40f)) + 10f, 0f, (j * (-40f)) + 10f);
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                GameObject clone = Instantiate(prefab, position, rotation);
                clone.name = prefab.name + "Clone " + j + "-"+ i;
                clone.transform.SetParent(houses);
            }
            for (int i = 0; i < 4; i++)
            {
                GameObject prefab = housePrefabs[2];
                Vector3 position = new Vector3((i * (-40f)) - 8f, 0f, (j * (-40f)) + 9f);
                Quaternion rotation = Quaternion.Euler(0, 180, 0);
                GameObject clone = Instantiate(prefab, position, rotation);
                clone.name = prefab.name + "Clone " + j + "-" + i;
                clone.transform.SetParent(houses);
            }
            for (int i = 0; i < 4; i++)
            {
                GameObject prefab = housePrefabs[3];
                Vector3 position = new Vector3((i * (-40f)) + 9f, 0f, (j * (-40f)) - 10f);
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                GameObject clone = Instantiate(prefab, position, rotation);
                clone.name = prefab.name + "Clone " + j + "-" + i;
                clone.transform.SetParent(houses);
            }
            for (int i = 0; i < 4; i++)
            {
                GameObject prefab = housePrefabs[1];
                Vector3 position = new Vector3((i * (-40f)) - 10f, 0f, (j * (-40f)) - 10f);
                Quaternion rotation = Quaternion.Euler(0, -90, 0);
                GameObject clone = Instantiate(prefab, position, rotation);
                clone.name = prefab.name + "Clone " + j + "-" + i;
                clone.transform.SetParent(houses);
            }
            for (int i = 1; i < 3; i++)
            {
                GameObject prefab = housePrefabs[4];
                Vector3 position = new Vector3((i * (40f) - 2f), 0f, (j * (-40f)));
                Quaternion rotation = Quaternion.Euler(0, 180, 0);
                GameObject clone = Instantiate(prefab, position, rotation);
                clone.name = prefab.name + "Clone " + j + "-" + i;
                clone.transform.SetParent(houses);
            }
        }
    }
}