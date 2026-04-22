using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    float remainigTime = 120;
    private bool hardmode;
    void Start()
    {
        hardmode = GameObject.Find("Difficulty").GetComponent<difficultyManager>().getDifficulty();
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
            //game over script;
        }
        
        int minutes = Mathf.FloorToInt(remainigTime / 60);
        int seconds = Mathf.FloorToInt(remainigTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
