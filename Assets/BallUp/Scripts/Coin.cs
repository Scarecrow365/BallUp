using UnityEngine;

public class Coin : MonoBehaviour
{
    [Range(0,1)][SerializeField] private float rotationSpeed;
    
    private void Update()
    {
        RotateCoin();
    }

    private void RotateCoin()
    {
        transform.Rotate(0f, rotationSpeed, 0f, Space.World);
    }
}