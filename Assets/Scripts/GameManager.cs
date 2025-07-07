using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float gameTime;
    [SerializeField] private float gameTimer;
    private CapturePoint capturePoint;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        capturePoint = FindAnyObjectByType<CapturePoint>();
        gameTimer = gameTime;
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        if(gameTimer <= 0.0f)
        {
            EndGame();
        }
    }

    public float GetTimer()
    {
        return gameTimer;
    }

    private void EndGame()
    {
        if (capturePoint.capturePercentRange > 0) Victory();
        else Lose();
    }

    public void Victory()
    {
        UIManager.Instance.ShowUI(GameUI.Win);
    }
    public void Lose()
    {
        UIManager.Instance.ShowUI(GameUI.Lose);
    }
}
