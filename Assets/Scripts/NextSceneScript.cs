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
            saveGameScript.SaveData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
