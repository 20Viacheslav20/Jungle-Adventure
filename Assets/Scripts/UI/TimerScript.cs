using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] Text timerText;

    public float CurrentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = $"Score: {CurrentTime.ToString("0")}";
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += 1 * Time.deltaTime;
        timerText.text = $"Score: {CurrentTime.ToString("0")}";
    }
}
