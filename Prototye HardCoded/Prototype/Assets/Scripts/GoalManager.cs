using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    public static GoalManager instance;

    public int goalsCollected = 0;
    public int totalGoals = 4;
    public Text goalText;
    public GameObject winPanel; //  Reference to Win Panel

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void GoalCollected()
    {
        goalsCollected++;
        goalText.text = "Goals: " + goalsCollected + "/" + totalGoals;

        if (goalsCollected >= totalGoals)
        {
            Timer.instance.StopTimer();
            if (winPanel != null)
                winPanel.SetActive(true); //  Show Win Screen
        }
    }
}
