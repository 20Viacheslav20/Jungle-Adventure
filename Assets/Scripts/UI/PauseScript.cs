using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseGameObject;
    
    public bool PauseGame;

    public void PauseResumeGame()
    {
        if (PauseGame)
        {
            Resume();
        } else
        {
            Pause();
        }
    }
    public void Resume()
    {
        pauseGameObject.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Pause()
    {
        pauseGameObject.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene(0);
    }

}
