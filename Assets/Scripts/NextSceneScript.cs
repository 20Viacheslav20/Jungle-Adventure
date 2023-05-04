using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{
    private SaveGameScript saveGameScript;
    // Start is called before the first frame update
    void Start()
    {
       saveGameScript = GameObject.FindGameObjectWithTag("Background").GetComponent<SaveGameScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            if (buildIndex == 3)
            {
                saveGameScript.SaveData();
                return;
            } else
            {
                saveGameScript.SaveData();
                SceneManager.LoadScene(buildIndex + 1);
            }
        }
        
    }
}
