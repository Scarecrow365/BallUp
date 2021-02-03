using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action OnFallAction = delegate { };

    [SerializeField] private float _sensitive;
    [SerializeField] private float _abyss;
    [SerializeField] private Ball _ball;

    private float _positionX;
    private Transform _ballTransform;
    
    //private float _borderLimitX;

    private void Awake()
    {
        _ballTransform = _ball.transform;
        //_borderLimitX = SetLimit();
    }

    private void Update()
    {
        Controller();
        GameOver();
    }

    private void Controller()
    {
        //BoundOfScreen();
        _positionX += Input.GetAxis("Mouse X") * _sensitive * Time.deltaTime;
        _ballTransform.position = new Vector2(_positionX, _ball.transform.position.y);
    }

    //private void BoundOfScreen()
    //{
    //    if (_borderLimitX < _ball.transform.position.x)
    //        _ball.transform.position = new Vector2(_borderLimitX, _ball.transform.position.y);
    //    else if (-_borderLimitX > transform.position.x)
    //        _ball.transform.position = new Vector2(-_borderLimitX, _ball.transform.position.y);
    //}

    //private float SetLimit()
    //{
    //    float zDistance = Camera.main.transform.position.z - _ball.transform.position.z;
    //    return _borderLimitX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0,
    //        -zDistance / Mathf.Cos(Camera.main.transform.localEulerAngles.x * Mathf.Deg2Rad))).x;
    //}
    private void GameOver()
    {
        if (transform.position.y <= _abyss)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            OnFallAction?.Invoke();
        }
    }
}