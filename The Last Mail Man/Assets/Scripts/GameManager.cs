using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

        // if(instance != null) {
        //     Destroy(gameObject);
        //     return;
        // }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        
    }
}