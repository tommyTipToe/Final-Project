using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public Toggle hard;

    public static GameManager instance;

    void Start()
    {
        // Add a listener to detect value changes via code
        hard.onValueChanged.AddListener(delegate {
            ToggleValueChanged(hard);
        });

        // Manually set the toggle state
        hard.isOn = false; 
    }

    void ToggleValueChanged(Toggle change)
    {
        Debug.Log("New Value Is: " + hard.isOn);
    }

    void Awake() {

        if(instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() {
        // Subscribe to the event
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        if(scene.buildIndex == 0) {
            hard = GameObject.Find("DifficultyToggle").GetComponent<Toggle>();
            score = 0;
        }
    }

    public void AddScore(int a) {
        score += a;
        Debug.Log("Score updated: " + score);
    }
}