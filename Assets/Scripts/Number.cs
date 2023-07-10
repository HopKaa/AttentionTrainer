using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    private int _value;
    private GameSessionManager _gameSessionManager;
    [SerializeField] private Text _text;

    public void Initialized(GameSessionManager gameSessionManager, int value, Rect fieldSize)
    {
        _gameSessionManager = gameSessionManager;
        SetRandomPosition(fieldSize);
        _value = value;
        _text.text = (value + 1).ToString();
    }

    private void SetRandomPosition(Rect fieldSize)
    {
        float randomX = Random.Range(0, fieldSize.width);
        float randomY = Random.Range(0, fieldSize.height);

        ((RectTransform)transform).anchoredPosition = new Vector2(randomX, randomY);
    }
    public void OnClick()
    {
        _gameSessionManager.HandleButtonClick(_value);
        Destroy(gameObject);
    }
}