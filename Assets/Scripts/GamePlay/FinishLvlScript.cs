using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLvlScript : MonoBehaviour
{
    [SerializeField] private bool isLastLvl;

    private SaveGameScript saveGameScript;

    // Start is called before the first frame update
    void Start()
    {
       saveGameScript = GameObject.FindGameObjectWithTag("Background").GetComponent<SaveGameScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;            
            saveGameScript.SaveData();
            if (isLastLvl)
            {
                SceneManager.LoadScene(1);
            } else 
            { 
                SceneManager.LoadScene(buildIndex + 1);     
            }
        }   
    }
}
