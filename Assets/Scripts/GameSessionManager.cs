using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSessionManager : MonoBehaviour
{
    [SerializeField] private Transform _panelTransform;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _resultText;
    [SerializeField ] private GameObject _buttonPrefab;
    [SerializeField] private Slider _slider;

    private int _minNumber = 1;
    private int _maxNumber = 9;
    private float _remainingTime;
    private int _currentNumberIndex;
    private bool _sessionInProgress;

    public float _sessionTime = 60f;
    public List<int> _numbersToClick = new List<int> { 1 };
    public void StartSession()
    {
        _remainingTime = _sessionTime;
        _progressBar.value = 1f;
        _resultText.text = "";
        _currentNumberIndex = 0;
        _sessionInProgress = true;

        SpawnButtons();
        StartCoroutine(Countdown());
    }

    void EndSession(bool success)
    {
        _sessionInProgress = false;
        StopCoroutine(Countdown());

        if (success)
        {
            _resultText.text = "Success";
        }
        else
        {
            _resultText.text = "Fail";
        }
    }
    public void RestartSession()
    {
        _resultText.text = "";

        StartSession();
    }
    IEnumerator Countdown()
    {
        while (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            _timerText.text = _remainingTime.ToString("F1");
            _progressBar.value = _remainingTime / _sessionTime;

            yield return null;
        }

        EndSession(false);
    }

    public void CheckClick(int clickedNumber)
    {
        if (!_sessionInProgress)
            return;

        if (clickedNumber == _numbersToClick[_currentNumberIndex])
        {
            _currentNumberIndex++;

            if (_currentNumberIndex >= _numbersToClick.Count)
            {
                EndSession(true);
            }
        }
        else
        {
            EndSession(false);
        }
    }

    public void SpawnButtons()
    {
        int numberOfButtons = (int)_slider.value;

        if (numberOfButtons < _minNumber)
        {
            numberOfButtons = _minNumber;
        }
        else if (numberOfButtons > _maxNumber)
        {
            numberOfButtons = _maxNumber;
        }

        int minValue = 1;
        int maxValue = numberOfButtons;

        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject buttonObject = Instantiate(_buttonPrefab);
            buttonObject.transform.SetParent(_panelTransform);

            RectTransform panelRect = _panelTransform.GetComponent<RectTransform>();
            Vector2 randomPosition = GetRandomPositionInPanel(panelRect);

            buttonObject.transform.localPosition = randomPosition;

            Text buttonText = buttonObject.GetComponentInChildren<Text>();
            buttonText.text = (minValue + i).ToString();

            buttonObject.GetComponent<Button>().onClick.AddListener(() => DestroyButton(buttonObject));
        }
    }

    private Vector2 GetRandomPositionInPanel(RectTransform panelRect)
    {
        float minX = panelRect.rect.xMin;
        float maxX = panelRect.rect.xMax;
        float minY = panelRect.rect.yMin;
        float maxY = panelRect.rect.yMax;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }

    private void DestroyButton(GameObject buttonObject)
    {
        Destroy(buttonObject);
    }
}
