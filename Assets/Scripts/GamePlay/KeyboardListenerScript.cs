using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardListenerScript : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private ItemCollectorScript collector;
    private PauseScript pauseScript;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collector = GetComponent<ItemCollectorScript>();
        pauseScript = GameObject.FindGameObjectWithTag("UI").GetComponent<PauseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseScript.PauseGame)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                pauseScript.PauseResumeGame();
            }

            if (Input.GetKey(KeyCode.R))
            {
                RestartLevel();
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        collector.CountOfLives = 4;
    }
}
