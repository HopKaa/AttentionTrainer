using UnityEngine;
using UnityEngine.UI;


public class GameSettingsManager : MonoBehaviour
{
    public Slider _numbersSlider;
    public Slider _timeSlider;
    public GameSessionManager _gameSessionManager;

    public void OnNumbersSliderChange()
    {
        int numbersCount = (int)_numbersSlider.value;
        _gameSessionManager._numbersToClick.Clear();

        for (int i = 1; i <= numbersCount; i++)
        {
            _gameSessionManager._numbersToClick.Add(i);
        }
    }

    public void OnTimeSliderChange()
    {
        float sessionTime = _timeSlider.value;
        _gameSessionManager._sessionTime = sessionTime;
    }
}
