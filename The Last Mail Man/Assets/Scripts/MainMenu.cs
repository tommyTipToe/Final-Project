using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void LoadGame() {
        // Changes scene to the game
        SceneManager.LoadScene(1);
    }

}
