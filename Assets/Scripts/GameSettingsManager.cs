using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameSettingsManager : MonoBehaviour
{
    public Slider numbersSlider; // Slider ��� ��������� ���������� ������������ �����
    public Slider timeSlider; // Slider ��� ��������� ������� ������� ������
    public GameSessionManager gameSessionManager; // ������ �� ������ GameSessionManager

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
