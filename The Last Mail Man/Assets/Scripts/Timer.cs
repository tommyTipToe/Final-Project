using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    float remainigTime = 120;
    private bool hardmode;
    void Start()
    {
        //hardmode = GameObject.Find("Difficulty").GetComponent<difficultyManager>().getDifficulty();
        hardmode = GameManager.instance.hard.isOn;
        if (hardmode){
            remainigTime = 90;
        }
            
    }

    void Update()
    {
        if (remainigTime > 0)
        {
            remainigTime -= Time.deltaTime;
        }
        else
        {
            remainigTime = 0;
            SceneManager.LoadScene(1);

        }
        
        int minutes = Mathf.FloorToInt(remainigTime / 60);
        int seconds = Mathf.FloorToInt(remainigTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
