using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSessionManager : MonoBehaviour
{
    private const float _defaultSessionTime = 60f;
    private const string _resultFail = "Fail";
    private const string _resultSuccess = "Success";

    [SerializeField] private RectTransform _panelTransform;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _resultText;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _timeSlider;

    private readonly List<Number> _numbers = new();

    private float _remainingTime;
    private int _currentNumberMax;
    private bool _sessionInProgress;
    private float _sessionTime = _defaultSessionTime;
    private int _currentNumber;

    private void EndSession(bool success)
    {
        _sessionInProgress = false;

        if (success)
        {
            _resultText.text = _resultSuccess;
        }
        else
        {
            _resultText.text = _resultFail;
        }
        StopAllCoroutines();
        _remainingTime = 0f;
        _progressBar.value = 0f;
    }
    private void StartSession()
    {
        InitValues();
        ClearPanel();
        SpawnNumbers();

        StartCoroutine(Countdown());
    }

    private void InitValues()
    {
        _currentNumber = 0;
        _remainingTime = _sessionTime;
        _progressBar.value = 1f;
        _resultText.text = string.Empty;
        _sessionInProgress = true;
    }
    private void ClearPanel()
    {
        foreach (Number number in _numbers)
        {
            if (number)
            {
                Destroy(number.gameObject);
            }
        }
        _numbers.Clear();
    }
    private IEnumerator Countdown()
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
    public void OnTimeSliderChange()
    {
        _sessionTime = _timeSlider.value;
    }

    private void SpawnNumbers()
    {
        _currentNumberMax = (int)_slider.value;

        for (int i = 0; i < _currentNumberMax; i++)
        {
            GameObject buttonObject = Instantiate(_buttonPrefab, _panelTransform);

            Rect _fieldSize = _panelTransform.rect;

            buttonObject.GetComponent<Number>().Initialized(this, i, _fieldSize);

            _numbers.Add(buttonObject.GetComponent<Number>());
        }
    }

    public void HandleButtonClick(int value)
    {
        if (!_sessionInProgress)
        {
            return;
        }    

        if(_currentNumber != value)
        {
            EndSession(false);
        }

        _currentNumber++;

        if (_currentNumber >= _currentNumberMax)
        {
            EndSession(true);
        }
    }
}