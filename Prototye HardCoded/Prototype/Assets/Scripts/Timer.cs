using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    private float timeElapsed;
    private bool running = false;
    public Text timerText;

    void Awake()
    {
        instance = this;
    }

    public void StartTimer()
    {
        timeElapsed = 0f;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    void Update()
    {
        if (!running) return;
        timeElapsed += Time.deltaTime;
        if (timerText != null)
            timerText.text = "Time: " + timeElapsed.ToString("F2");
    }
}
