using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectorScript : MonoBehaviour
{
    [SerializeField] private Text gemsText;
    [SerializeField] private Text livesText;


    private int countOfGemsOnLevel;

    public int CountOfGems = 0;
    public int CountOfLives = 4;

    void Start()
    {
        countOfGemsOnLevel = GameObject.FindGameObjectsWithTag("Gem").Length;
        UpdateText();
    }

    private void Update()
    {
        UpdateText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Animator>().SetTrigger("ItemFeedback");
            Destroy(collision.gameObject, 0.2f);
            CountOfGems++;         
        }

        if (collision.gameObject.CompareTag("Cherry"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Animator>().SetTrigger("ItemFeedback");
            Destroy(collision.gameObject, 0.2f); 
            CountOfLives++;            
        }

    }

    private void UpdateText()
    {
        gemsText.text = $"Gems: {CountOfGems}/{countOfGemsOnLevel}";
        livesText.text = $"Lives: {CountOfLives}";
    }
}
