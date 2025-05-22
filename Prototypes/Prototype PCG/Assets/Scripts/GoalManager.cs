using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    public int totalGoals = 4;
    private int collectedGoals = 0;

    public Text goalText;             // Legacy UI Text
    public GameObject winScreen;      // Assign a UI Panel or Text in the Inspector
    public Timer timer;               // Reference to your Timer script

    void Start()
    {
        UpdateGoalText();
    }

    public void GoalCollected()
    {
        collectedGoals++;
        UpdateGoalText();

        if (collectedGoals >= totalGoals)
        {
            Debug.Log("All goals collected!");
            if (winScreen != null)
                winScreen.SetActive(true);

            if (timer != null)
                timer.StopTimer();
        }
    }

    void UpdateGoalText()
    {
        if (goalText != null)
            goalText.text = $"Goals: {collectedGoals}/{totalGoals}";
    }
}
