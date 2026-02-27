using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public static LaneManager Instance;

    public int laneCount = 4;
    public float[] lanePositions;
    public Transform[] laneTransforms;

    void Awake()
    {
        Instance = this;

        if (laneTransforms == null || laneTransforms.Length == 0)
        {
            Debug.LogError("LaneTransforms not assigned!");
            return;
        }

        for (int i = 0; i < laneTransforms.Length; i++)
        {
            if (laneTransforms[i] == null)
            {
                Debug.LogError("Lane Transform at index " + i + " is NULL");
            }
        }

        laneCount = laneTransforms.Length;
        CalculateLanes();
    }

    void CalculateLanes()
    {
        laneCount = laneTransforms.Length;
        lanePositions = new float[laneCount];

        for (int i = 0; i < laneCount; i++)
        {
            lanePositions[i] = laneTransforms[i].position.x;
        }
    }

    public float GetLaneX(int laneIndex)
    {
        return lanePositions[laneIndex];
    }
}