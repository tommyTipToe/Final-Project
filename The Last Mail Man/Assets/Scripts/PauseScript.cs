using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public GameObject container;
    private CarInputActions carControls;

    void Update()
    {
        bool pauseInput = carControls.Car.Pause.IsPressed();
        if (pauseInput) {
            container.SetActive(true);
            Time.timeScale = 0;
        }
        if (carControls.Car.DebugAddScore.IsPressed()) {
            GameManager.instance.AddScore(1000);
        }
    }

    void Awake()
    {
        carControls = new CarInputActions(); // Initialize Input Actions
    }
    void OnEnable()
    {
        carControls.Enable();
    }

    void OnDisable()
    {
        carControls.Disable();
    }

    public void ResumeButton() {
        container.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
