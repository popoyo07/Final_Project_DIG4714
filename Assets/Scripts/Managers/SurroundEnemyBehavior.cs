using UnityEngine;

public class SurroundEnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public float surroundRadius = 8f;
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool hasReachedPosition = false;
    private static int totalSurroundingEnemies = 0;
    private int myPositionIndex;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("[SurroundEnemyBehavior] Player reference not set!");
            return;
        }

        myPositionIndex = totalSurroundingEnemies++;
        CalculateTargetPosition();
    }

    private void Update()
    {
        if (hasReachedPosition || player == null) return;

        // Update target position as player moves
        CalculateTargetPosition();

        // Move towards target position
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Check if we've reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            hasReachedPosition = true;
            // Look at player
            transform.LookAt(player.transform);
        }
    }

    private void CalculateTargetPosition()
    {
        float angle = (360f / totalSurroundingEnemies) * myPositionIndex;
        float radian = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(radian) * surroundRadius,
            0,
            Mathf.Sin(radian) * surroundRadius
        );

        targetPosition = player.transform.position + offset;
        targetPosition.y = transform.position.y; // Keep the same height
    }

    private void OnDestroy()
    {
        totalSurroundingEnemies--;
    }
}


