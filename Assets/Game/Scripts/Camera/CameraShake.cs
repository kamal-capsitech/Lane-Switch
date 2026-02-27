using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    public float duration = 0.2f;
    public float magnitude = 0.2f;

    float currentDuration = 0f;
    Vector3 originalLocalPos;

    private void Awake()
    {
        Instance = this;
        originalLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (currentDuration > 0)
        {
            transform.localPosition =
                originalLocalPos + (Vector3)Random.insideUnitCircle * magnitude;

            currentDuration -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = originalLocalPos;
        }
    }

    public void Shake()
    {
        currentDuration = duration;
    }
}