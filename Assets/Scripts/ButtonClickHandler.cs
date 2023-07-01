using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    private GameSessionManager _gameSessionManager;

    private void Start()
    {
        _gameSessionManager = FindObjectOfType<GameSessionManager>();
    }

    public void OnButtonClick()
    {
        if (_gameSessionManager != null)
        {
            _gameSessionManager.HandleButtonClick(gameObject);
        }
    }
}