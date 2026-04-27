using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void LoadSuburbs() {
        // Changes scene to the suburbs scene
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void LoadMoon() {
        // Changes scene to the moon scene
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
    }

}
