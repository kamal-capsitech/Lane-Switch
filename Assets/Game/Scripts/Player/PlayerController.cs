using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int playerHealth = 3;
    public Text playerHealthText;
    public static PlayerController Instance { get; private set; }
    public float moveDuration = 0.1f;

    int currentLane = 0;
    int laneCount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int PlayerReduceHealth()
    {
        playerHealth--;
        playerHealthText.text = playerHealth.ToString();
        return playerHealth;
    }
    public int PlayerImproveHealth()
    {
        playerHealth++;
        playerHealthText.text = playerHealth.ToString();
        return playerHealth;
    }

    void Start()
    {
        playerHealthText.text = playerHealth.ToString();
        laneCount = LaneManager.Instance.laneCount;
        transform.position = new Vector3(
            LaneManager.Instance.GetLaneX(currentLane),
            transform.position.y,
            0);
    }

    void Update()
    {
        if (!GameManager.IsGameStart) return;

        SwitchLane();


    }

    public float forwardSpeed = 3f;

    public float speedIncreaseRate = 0.1f;


    void SwitchLane()
    {

        if (Input.GetMouseButtonDown(0) && !GameManager.IsGameOver)
        {
            GameManager.IsPlayerStarted = true;

            currentLane++;
            if (currentLane >= laneCount)
                currentLane = 0;

            float targetX = LaneManager.Instance.GetLaneX(currentLane);

            Vector3 pos = transform.position;
            pos.x = targetX;
            transform.position = pos;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            Debug.Log("Collected a star!");
            ScoreManager.Instance.AddStar();
        }
        else if (collision.CompareTag("Health"))
        {
            Debug.Log("Collected a health!");
            PlayerImproveHealth();
        }
    }
    public ParticleSystem obstaclecollisioneffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boom"))
        {
            Debug.Log("Hit an obstacle!");
            obstaclecollisioneffect.gameObject.SetActive(true);
            obstaclecollisioneffect.Play();
            GameManager.Instance.GameOver();
            ScoreManager.Instance.HighScore();
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Arrow") ||
            collision.gameObject.CompareTag("Box"))
        {
            StartCoroutine(PlayerHealthAnim());
            collision.gameObject.SetActive(false);
            Debug.Log("Hit an enemy!");
            PlayerReduceHealth();
            if (playerHealth <= 0)
            {
                obstaclecollisioneffect.gameObject.SetActive(true);
                obstaclecollisioneffect.Play();
                GameManager.Instance.GameOver();
                ScoreManager.Instance.HighScore();
            }
        }
    }

    IEnumerator PlayerHealthAnim()
    {
        
            yield return new WaitForSeconds(.2f);
            transform.DOScale(0.1f, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                transform.DOScale(0.3f, 0.2f).SetEase(Ease.InBack);
            });

    }
}