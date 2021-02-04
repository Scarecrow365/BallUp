using UnityEngine;

public class BaseWindow : MonoBehaviour
{
    [SerializeField] private GameState showScreenInState;

    public GameState GetWindowState => showScreenInState;
    
    public virtual void SetData(){}
}