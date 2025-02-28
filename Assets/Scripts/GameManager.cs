using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;

    private Player player;
    private Spawner spawner;

    private PowerupSpawner powerupSpawner;

    private GroundSpawn terrainSpawner;
    public bool terrainReturn = true;

    private float score;
    public float Score => score;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        powerupSpawner = FindObjectOfType<PowerupSpawner>();
        terrainSpawner = FindAnyObjectByType<GroundSpawn>();

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        Powerup[] powerups = FindObjectsOfType<Powerup>();

        foreach (var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }

        foreach (var powerup in powerups) {
            Destroy(powerup.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        spawner.gameObject.SetActive(true);
        powerupSpawner.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        terrainSpawner.gameObject.SetActive(true);
        terrainSpawner.transform.position = new Vector3(-9.6f,0.17f,0.01619219f);
        StartCoroutine(moveBack(2.0f));

        terrainSpawner.Spawn();
        terrainReturn = false;
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        UpdateHiscore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

    public float getScore()
    {
        return score;
    }

    public void addScore(float amount)
    {
        score += amount;
    }

    public bool getGameOver()
    {
        if (enabled == false)
        {
            return true;
        }
        return false;
    }

    private IEnumerator moveBack(float duration) {
        yield return new WaitForSeconds(duration);
        terrainSpawner.transform.position = new Vector3(9.37f,0.17f,0.01619219f);
        terrainReturn = false;

    }
    

}


