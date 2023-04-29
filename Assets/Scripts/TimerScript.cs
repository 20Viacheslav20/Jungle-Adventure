using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] Text timerText;
    private float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = $"Score: {currentTime.ToString("0")}";
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        timerText.text = $"Score: {currentTime.ToString("0")}";
    }
}
