using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    static public bool level1;
    static public bool level2;

    public void LoadLevel(string name)
    {
        Time.timeScale = 1;
        print("Load scene: " + name);

        if (name != "Final Level")
            SceneManager.LoadScene(name);
        else if (level1 && level2)
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
