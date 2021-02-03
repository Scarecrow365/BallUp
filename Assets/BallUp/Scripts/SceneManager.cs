using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SceneManager : MonoBehaviour
{
    [NonSerialized] public int _JumpScore;
    [NonSerialized] public int _CoinScore;
    [NonSerialized] public int jumpcounter;
    [NonSerialized] public int coincounter;
    private int speedUp;

    [SerializeField] private GameObject _ParticlesJump;
    [SerializeField] private GameObject _ParticlesCoin;

    private float _limit = 20f;

    public UiView HudManager;
    public static SceneManager instance;

    void Awake()
    {
        //Time.timeScale = 0f;
        if (instance == null)
            instance = this;

        else if (instance != null)
        {
            instance.HudManager = FindObjectOfType<UiView>();
            Destroy(gameObject);
        }
    }

    public void IncreaseJumpScore(int amount)
    {
        speedUp += amount;
        _JumpScore += amount;
        if (HudManager != null)
            HudManager.JumpScoreCount(amount);
        if (speedUp >= 10)
        {
            Time.timeScale += 0.1f;
            speedUp = 0;
        }
    }

    public void IncreaseCoinScore(int amount)
    {
        _CoinScore += amount;
        if (HudManager != null)
            HudManager.CoinScoreCount(amount);
    }

    public void JumpVFX(int amount)
    {
        jumpcounter += amount;
        if (jumpcounter >= 3)
        {
            Instantiate(_ParticlesJump, new Vector3((Random.Range(-_limit, _limit)), 4, 20), transform.rotation);
            jumpcounter = 0;
        }
    }

    public void CoinVFX(int amount)
    {
        coincounter += amount;
        if (coincounter >= 5)
        {
            Instantiate(_ParticlesCoin, new Vector3(Random.Range(-_limit, _limit), 4, 25), transform.rotation);
            coincounter = 0;
        }
    }

    public void ToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UI");
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void Play()
    {
        _JumpScore = 0;
        _CoinScore = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
}