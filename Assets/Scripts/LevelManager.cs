using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{


    public void LoadLevel(string name)
    {
        Time.timeScale = 1;
        print("Load scene: " + name);
        SceneManager.LoadScene(name);
    }

    public void ReloadLevel()
    {
        string scene = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
    public void QuitLevel()
    {
       
        Application.Quit();
    }


}
