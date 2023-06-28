using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSessionManager : MonoBehaviour
{
    public float _sessionTime = 60f;
    public float _remainingTime;
    public Slider _progressBar;
    public Text _timerText;
    public Text _resultText;
    public List<int> _numbersToClick = new List<int> { 1 };
    private int _currentNumberIndex;
    private bool _sessionInProgress;

    public void StartSession()
    {
        _remainingTime = _sessionTime;
        _progressBar.value = 1f;
        _resultText.text = "";
        _currentNumberIndex = 0;
        _sessionInProgress = true;

        
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
}
