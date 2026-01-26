using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
    }

    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
    }
}
