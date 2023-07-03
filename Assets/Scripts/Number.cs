using UnityEngine;

public class Number : MonoBehaviour
{
    public void OnClick()
    {
        if (_gameSessionManager != null)
        {
            _gameSessionManager.HandleButtonClick(gameObject);
        }
    }
    public void Initialized(GameSessionManager gameSessionManager)
    {
        _gameSessionManager = gameSessionManager;

    }
    private GameSessionManager _gameSessionManager;
}