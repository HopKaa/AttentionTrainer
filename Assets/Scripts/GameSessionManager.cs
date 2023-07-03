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

    private List<int> _numbersToClick = new List<int> ();
    private List<int> _clickedNumbers = new List<int> ();

    private void StartSession()
    {
        _remainingTime = _sessionTime;
        _progressBar.value = 1f;
        _resultText.text = "";
        _currentNumberIndex = 0;
        _sessionInProgress = true;

        StartCoroutine(Countdown());
    }

    private void EndSession(bool success)
    {
        _sessionInProgress = false;

        if (success)
        {
            _resultText.text = "Success";
        }
        else
        {
            _resultText.text = "Fail";
        }
        StopAllCoroutines();
        _remainingTime = 0f;
        _progressBar.value = 0f;
    }
    private void RestartSession()
    {
        _resultText.text = "";
        ClearPanel();
        SpawnButtons();
        _clickedNumbers.Clear();
        StartSession();
    }
    private void ClearPanel()
    {
        foreach (Transform button in _panelTransform)
        {
            Destroy(button.gameObject);
        }
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

    private void CheckClick(int clickedNumber)
    {
        if (!_sessionInProgress)
            return;

        if (clickedNumber != _numbersToClick[_currentNumberIndex])
        {
            EndSession(false);
            return;
        }

        _currentNumberIndex++;

        _clickedNumbers.Add(clickedNumber);

        if (_currentNumberIndex >= _numbersToClick.Count)
        {
            EndSession(true);
        }
    }

    private void SpawnButtons()
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

        _numbersToClick.Clear();

        for (int i = 1; i <= numberOfButtons; i++)
        {
            _numbersToClick.Add(i);
        }

        _numbersToClick = new List<int>();

        int maxValue = (int)_slider.value;

        if (maxValue < _minNumber)
        {
            maxValue = _minNumber;
        }
        else if (maxValue > _maxNumber)
        {
            maxValue = _maxNumber;
        }

        for (int i = 1; i <= maxValue; i++)
        {
            _numbersToClick.Add(i);
        }

        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject buttonObject = Instantiate(_buttonPrefab);
            buttonObject.transform.SetParent(_panelTransform);
            buttonObject.GetComponent<Number>().Initialized(this);

            RectTransform panelRect = _panelTransform.GetComponent<RectTransform>();
            Vector2 randomPosition = GetRandomPositionInPanel(panelRect);

            buttonObject.transform.localPosition = randomPosition;

            Text buttonText = buttonObject.GetComponentInChildren<Text>();
            buttonText.text = _numbersToClick[i].ToString();
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

    public void HandleButtonClick(GameObject buttonObject)
    {
        if (!_sessionInProgress)
            return;

        int clickedNumber = int.Parse(buttonObject.GetComponentInChildren<Text>().text);
        CheckClick(clickedNumber);
      
        Destroy(buttonObject);
    }
}
