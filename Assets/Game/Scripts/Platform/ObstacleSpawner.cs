using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject boxPrefab;
    public GameObject starPrefab;
    public GameObject boomPrefab;
    public GameObject arrowPrefab;
    public GameObject healthPrefab;




    [Header("Spawn Settings")]
    public float spawnInterval = 1f;
    public float spawnY = 10f;
    public float baseSpeed = 6f;

    [Header("Pool Sizes")]

    public int boxPoolSize = 10;
    public int starPoolSize = 10;
    public int boomPoolSize = 10;
    public int arrowPoolSize = 10;



    private Queue<GameObject> boxPool = new Queue<GameObject>();
    private Queue<GameObject> starPool = new Queue<GameObject>();
    private Queue<GameObject> boomPool = new Queue<GameObject>();
    private Queue<GameObject> arrowPool = new Queue<GameObject>();

    float timer;

    void Start()
    {

        CreatePool(boxPrefab, boxPool, boxPoolSize);
        CreatePool(starPrefab, starPool, starPoolSize);
        CreatePool(boomPrefab, boomPool, boomPoolSize);
        CreatePool(arrowPrefab, arrowPool, arrowPoolSize);


    }
   float spentTime = 0f;

    void Update()
    {
        if (!GameManager.IsGameStart) return;
        if (GameManager.IsGameOver) return;

        timer += Time.deltaTime;
        spentTime += Time.deltaTime;
        string timeString = spentTime.ToString("0.00"); // Fixed: use ToString instead of TryFormat
        ScoreManager.Instance.UpdateScore(timeString);
        if (timer >= spawnInterval)
        {
            SpawnPattern();
            timer = 0f;
        }
    }

    void CreatePool(GameObject prefab, Queue<GameObject> pool, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }



    void SpawnPattern()
    {
        int randomValue = Random.Range(0, 100);
        float yPos = spawnY;

        if (randomValue < 30)              // 50% BOX
        {
            SpawnFromPool(boxPool, yPos);
        }
        else if (randomValue < 60)         // 20% STAR
        {
            SpawnFromPool(starPool, yPos);
        }
        else if (randomValue < 80)         // 20% BOOM
        {
            SpawnFromPool(boomPool, yPos);
        }
        else if (randomValue > 80)                               // 10% ARROW
        {
            SpawnFromPool(arrowPool, yPos);
        }
    }


    void SpawnFromPool(Queue<GameObject> pool, float yPos)
    {
        if (pool.Count == 0) return;

        GameObject obj = pool.Dequeue();

        int randomLane = Random.Range(0, LaneManager.Instance.laneCount);
        float x = LaneManager.Instance.GetLaneX(randomLane);

        obj.transform.position = new Vector3(x, yPos, 0);
        obj.SetActive(true);

        obj.GetComponent<Obstacle>().Activate(baseSpeed);

        pool.Enqueue(obj);
    }

    
}