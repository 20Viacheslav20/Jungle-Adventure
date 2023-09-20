using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLvlScript : MonoBehaviour
{
    [SerializeField] private bool isLastLvl;

    private SaveGameScript saveGameScript;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        saveGameScript = GameObject.FindGameObjectWithTag("Background").GetComponent<SaveGameScript>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("isPlayerTouched");
            audioSource.Play();
            StartCoroutine(DelayedAction());
        }   
    }

    private IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(1.5f); 

        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        saveGameScript.SaveData();

        if (isLastLvl)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(buildIndex + 1);
        }
    }
}
