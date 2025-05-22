using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GoalManager manager = FindObjectOfType<GoalManager>();
            if (manager != null)
            {
                manager.GoalCollected();
            }

            Destroy(gameObject);
        }
    }
}
