using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    private int _value;
    private GameSessionManager _gameSessionManager;

    public void OnClick()
    {
        _gameSessionManager.HandleButtonClick(_value);
        Destroy(gameObject);
    }

    public void Initialized(GameSessionManager gameSessionManager, int value, Rect fieldSize)
    {
        _gameSessionManager = gameSessionManager;
        SetRandomPosition(fieldSize);
        _value = value;
        Text buttonText = GetComponentInChildren<Text>();
        buttonText.text = (value + 1).ToString();
    }

    private void SetRandomPosition(Rect fieldSize)
    {
        float minX = _gameSessionManager._field.transform.position.x - fieldSize.width;
        float maxX = _gameSessionManager._field.transform.position.x + fieldSize.width;
        float minY = _gameSessionManager._field.transform.position.y - fieldSize.height;
        float maxY = _gameSessionManager._field.transform.position.y + fieldSize.height;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        transform.position = new Vector3(randomX, randomY, 0f);
    }
}