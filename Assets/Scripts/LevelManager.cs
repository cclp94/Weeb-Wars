using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{


    public void LoadLevel(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public void QuitLevel()
    {
       
        Application.Quit();
    }


}
