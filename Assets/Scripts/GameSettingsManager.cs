using UnityEngine;
using UnityEngine.UI;


public class GameSettingsManager : MonoBehaviour
{
    public Slider _timeSlider;
    public GameSessionManager _gameSessionManager;

    public void OnTimeSliderChange()
    {
        float sessionTime = _timeSlider.value;
        _gameSessionManager._sessionTime = sessionTime;
    }
}
