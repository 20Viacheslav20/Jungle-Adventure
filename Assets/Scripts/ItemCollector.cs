using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text gemsText;

    [SerializeField] private Text livesText;

    private int countOfGems = 0;
    public int CountOfLives = 4;

    void Start()
    {
        gemsText.text = $"Gems: {countOfGems}/{4}";
        livesText.text = $"Lives: {CountOfLives}";
    }

    private void Update()
    {
        gemsText.text = $"Gems: {countOfGems}/{4}";
        livesText.text = $"Lives: {CountOfLives}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            Destroy(collision.gameObject);
            countOfGems++;
            gemsText.text = $"Gems: {countOfGems}/{4}";
        }

        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject); 
            CountOfLives++;
            livesText.text = $"Lives: {CountOfLives}";
        }

    }

    
}
