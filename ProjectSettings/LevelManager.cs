using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{


    public void LoadLevel(string name)
    {
       
        SceneManager.LoadScene(name);
    }

    public void QuitLevel()
    {
       
        Application.Quit();
    }

}
