using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        Enemy,
        Boom,
        Stone,
        Health,
        Ammo,
        Star,
        Arrow
    }

    [Header("Setup")]
    public ObstacleType obstacleType;
    public float moveSpeed = 6f;

    [Header("Destroy Settings")]
    public float destroyY = -10f;

    bool isActive = false;

    public void Activate(float speed)
    {
        moveSpeed = speed;
        isActive = true;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isActive) return;

        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (transform.position.y <= destroyY)
        {
            Deactivate();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        HandleCollision();
        Deactivate();
    }

    void HandleCollision()
    {
        switch (obstacleType)
        {
            case ObstacleType.Enemy:
                Debug.Log("Hit Enemy");
                break;

            case ObstacleType.Boom:
                Debug.Log("Boom Explosion");
                break;

            case ObstacleType.Stone:
                Debug.Log("Hit Stone");
                break;

            case ObstacleType.Health:
                Debug.Log("Health Collected");
                break;

            case ObstacleType.Ammo:
                Debug.Log("Ammo Collected");
                break;

            case ObstacleType.Star:
                Debug.Log("Star Collected");
                break;
            case ObstacleType.Arrow:
                Debug.Log("Arrow Collected");
                break;
        }
    }

    void Deactivate()
    {
        isActive = false;
        gameObject.SetActive(false);   // pooling friendly
    }
}