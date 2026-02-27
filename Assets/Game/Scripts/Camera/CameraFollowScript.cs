using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public float yOffset = 2f;

    void LateUpdate()
    {
        if (target == null) return;
        if (GameManager.IsGameOver) return;

        transform.position = new Vector3(0f,target.position.y + yOffset,-10f);
    }
}