using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSessionManager : MonoBehaviour
{
    public float sessionTime = 60f; // Время одной игровой сессии
    public float remainingTime; // Оставшееся время игровой сессии
    public Slider progressBar; // Компонент Slider для отображения прогресса игровой сессии
    public Text timerText; // Компонент Text для отображения таймера
    public Text resultText; // Компонент Text для отображения результата игровой сессии
    public List<int> numbersToClick = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // Список чисел, которые нужно кликнуть в правильном порядке
    private int currentNumberIndex; // Индекс текущего числа в списке
    private bool sessionInProgress; // Флаг, указывающий на то, идет ли игровая сессия в данный момент

    public void StartSession()
    {
        // Инициализация переменных
        remainingTime = sessionTime;
        progressBar.value = 1f;
        resultText.text = "";
        currentNumberIndex = 0;
        sessionInProgress = true;

        // Запуск корутины для отсчета времени игровой сессии
        StartCoroutine(Countdown());
    }

    void EndSession(bool success)
    {
        sessionInProgress = false;
        StopCoroutine(Countdown());

        if (success)
        {
            resultText.text = "Success";
        }
        else
        {
            resultText.text = "Fail";
        }
    }
    public void RestartSession()
    {
        // Скрыть надпись с результатом игровой сессии
        resultText.text = "";

        // Запустить новую игровую сессию
        StartSession();
    }
    IEnumerator Countdown()
    {
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = remainingTime.ToString("F1");
            progressBar.value = remainingTime / sessionTime;

            yield return null;
        }

        EndSession(false); // Истекло время
    }

    public void CheckClick(int clickedNumber)
    {
        if (!sessionInProgress)
            return;

        if (clickedNumber == numbersToClick[currentNumberIndex])
        {
            currentNumberIndex++;

            if (currentNumberIndex >= numbersToClick.Count)
            {
                EndSession(true); // Все числа кликнуты в правильном порядке
            }
        }
        else
        {
            EndSession(false); // Неправильный клик на числе
        }
    }
}
