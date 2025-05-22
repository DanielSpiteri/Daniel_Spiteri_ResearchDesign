using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Goal reached!");

            // Increase goal count
            GoalManager.instance.GoalCollected();

            // Destroy this goal
            Destroy(gameObject);
        }
    }
}
