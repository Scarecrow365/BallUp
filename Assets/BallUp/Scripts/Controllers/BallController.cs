using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public event Action OnFallAction;
    public event Action OnCoinGet;

    [SerializeField] private Ball ballPrefab;
    [SerializeField] private float sensitive = 6;

    private const float GameOverYZone = -3f;
    private readonly Vector3 _spawnBallPosition = new Vector3(0, 0.3f, 0);
    private float _positionX;
    private Transform _ballTransform;
    private GameState _state;
    private int _coinCount;

    public int GetCoinInfo => _coinCount;

    public void Init()
    {
        InstantiateBall();
    }

    public void SetGameState(GameState gameState) => _state = gameState;

    private void Update()
    {
        if (_state == GameState.Game)
        {
            Controller();
            CheckYPosition();
        }
    }

    private void InstantiateBall()
    {
        var ball = Instantiate(ballPrefab, _spawnBallPosition, Quaternion.identity);
        ball.Init();
        ball.OnCoinGet += CoinGrab;
        _ballTransform = ball.transform;
    }

    private void CoinGrab()
    {
        _coinCount++;
        OnCoinGet?.Invoke();
    }

#if UNITY_EDITOR
    private void Controller()
    {
        _positionX += Input.GetAxis("Mouse X") * sensitive * Time.deltaTime;
        _ballTransform.position = new Vector2(_positionX, _ballTransform.position.y);
    }
#else
    private void Controller()
    {
        Vector3 accel = Input.acceleration;
        _positionX += accel.x * sensitive * Time.deltaTime;
        _ballTransform.position = new Vector2(_positionX, _ballTransform.position.y);
    }
#endif

    private void CheckYPosition()
    {
        if (_ballTransform.position.y <= GameOverYZone)
        {
            OnFallAction?.Invoke();
        }
    }
}