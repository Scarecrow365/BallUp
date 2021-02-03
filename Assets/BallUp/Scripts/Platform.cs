using System;
using UnityEngine;

public class Platform : MonoBehaviour, IPooledObject
{
    public event Action<Platform> OnPlatformDisable;

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            OnPlatformDisable?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}