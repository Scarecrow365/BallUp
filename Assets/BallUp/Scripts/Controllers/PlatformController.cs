using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private const float Speed = 5f;
    private List<Platform> _platforms;
    private int _disabledPlatform;
    private GameState _state;
    
    public event Action OnPlatformDisable;
    public event Action<int> OnDisablePlatformsCountChange;

    public void SetGameState(GameState gameState) => _state = gameState;

    private void Update()
    {
        if (_state == GameState.Game)
        {
            MovePlatform();
        }
    }

    private void RequestSpawnNewPlatform(Platform platform)
    {
        _disabledPlatform++;
        OnPlatformDisable?.Invoke();
        OnDisablePlatformsCountChange?.Invoke(_disabledPlatform);
        platform.OnPlatformDisable -= RequestSpawnNewPlatform;
    }

    private void RemoveDisablePlatform(Platform platform)
    {
        for (var index = 0; index < _platforms.Count; index++)
        {
            var obj = _platforms[index];
            if (obj == platform)
            {
                obj.OnPlatformDisable -= RemoveDisablePlatform;
                _platforms.Remove(obj);
            }
        }
    }

    private void MovePlatform()
    {
        var movementZ = Speed * Time.deltaTime;
        foreach (var platform in _platforms)
        {
            platform.transform.Translate(0, 0, -movementZ);
        }
    }

    public void InitStartGame(List<Platform> platforms)
    {
        _platforms = platforms;
        foreach (var platform in platforms)
        {
            platform.OnPlatformDisable += RequestSpawnNewPlatform;
            platform.OnPlatformDisable += RemoveDisablePlatform;
        }
    }

    public void GetNewPlatform(Platform newPlatform)
    {
        _platforms.Add(newPlatform);
        newPlatform.OnPlatformDisable += RequestSpawnNewPlatform;
        newPlatform.OnPlatformDisable += RemoveDisablePlatform;
    }

    public void OnDestroy()
    {
        foreach (var platform in _platforms)
        {
            platform.OnPlatformDisable -= RemoveDisablePlatform;
        }
    }
}