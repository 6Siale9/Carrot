using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{

    public GameObject container;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void mainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(null); //-------------- Ajouter Main Menu Scene
    }

    public void resumeButton()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }
}
