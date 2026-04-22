using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    public static GameManager instance;

    void Awake() {
        instance = this;
    }

    private void Update() {
        /*if(SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0) && Input.GetKeyDown(KeyCode.Space)) {
        *    SceneManager.LoadScene(0);
        } */
    }
}
