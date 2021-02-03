using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5;
    private Rigidbody _rb;
    public event Action OnCoinGet;

    public void Init()
    {
        _rb = GetComponent<Rigidbody>();
        Jump();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Platform"))
        {
            Jump();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Coin"))
        {
            col.gameObject.SetActive(false);
            OnCoinGet?.Invoke();
        }
    }

    private void Jump()
    {
        var jumpVector = new Vector3(0, jumpForce, 0);
        _rb.AddForce(jumpVector, ForceMode.Impulse);
    }
}