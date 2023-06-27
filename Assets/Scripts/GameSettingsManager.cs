using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameSettingsManager : MonoBehaviour
{
    public Slider numbersSlider; // Slider дл€ настройки количества отображаемых чисел
    public Slider timeSlider; // Slider дл€ настройки времени игровой сессии
    public GameSessionManager gameSessionManager; // —сылка на скрипт GameSessionManager

    public void OnNumbersSliderChange()
    {
        int numbersCount = (int)numbersSlider.value;
        gameSessionManager.numbersToClick.Clear();

        for (int i = 1; i <= numbersCount; i++)
        {
            gameSessionManager.numbersToClick.Add(i);
        }
    }

    public void OnTimeSliderChange()
    {
        float sessionTime = timeSlider.value;
        gameSessionManager.sessionTime = sessionTime;
    }
}
