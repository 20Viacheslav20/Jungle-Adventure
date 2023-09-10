using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private bool pauseGame;
    [SerializeField] private GameObject pauseGameObject;

    public void PauseResumeGame()
    {
        if (pauseGame)
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
        pauseGame = false;
    }

    public void Pause()
    {
        pauseGameObject.SetActive(true);
        Time.timeScale = 0f;
        pauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.LoadScene(0);
    }

}
