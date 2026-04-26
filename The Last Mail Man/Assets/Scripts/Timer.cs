using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    float remainingTime = 120;
    private bool hardmode;
    private bool triggered = false;
    private float[] xwind = {1, 1, -1, -1};
    private float[] zwind = { 1, -1, 1, -1 };
    private int index = 0;

    void Start()
    {
        //hardmode = GameObject.Find("Difficulty").GetComponent<difficultyManager>().getDifficulty();
        hardmode = GameManager.instance.hard.isOn;
        if (hardmode){
            remainingTime = 90;
        }
            
    }

    void Update()
    {

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (Mathf.FloorToInt(remainingTime) % 15 == 0)
            {
                if (!triggered)
                {
                    Vector3 change = new Vector3(xwind[index % xwind.Length], 0, zwind[index % zwind.Length]);
                    WeatherManager.instance.ChangeWind(change);
                    triggered = true;
                    index++;
                }
            }
            else
            {
                triggered = false;
            }

        }
        else
        {
            remainingTime = 0;
            if(GameManager.instance.score >= 500) {
                SceneManager.LoadScene(3);
            }
            else {
                SceneManager.LoadScene(2);
            }
        }
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
