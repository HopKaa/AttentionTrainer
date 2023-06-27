using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSessionManager : MonoBehaviour
{
    public float sessionTime = 60f; // ����� ����� ������� ������
    public float remainingTime; // ���������� ����� ������� ������
    public Slider progressBar; // ��������� Slider ��� ����������� ��������� ������� ������
    public Text timerText; // ��������� Text ��� ����������� �������
    public Text resultText; // ��������� Text ��� ����������� ���������� ������� ������
    public List<int> numbersToClick = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // ������ �����, ������� ����� �������� � ���������� �������
    private int currentNumberIndex; // ������ �������� ����� � ������
    private bool sessionInProgress; // ����, ����������� �� ��, ���� �� ������� ������ � ������ ������

    public void StartSession()
    {
        // ������������� ����������
        remainingTime = sessionTime;
        progressBar.value = 1f;
        resultText.text = "";
        currentNumberIndex = 0;
        sessionInProgress = true;

        // ������ �������� ��� ������� ������� ������� ������
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
        // ������ ������� � ����������� ������� ������
        resultText.text = "";

        // ��������� ����� ������� ������
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

        EndSession(false); // ������� �����
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
                EndSession(true); // ��� ����� �������� � ���������� �������
            }
        }
        else
        {
            EndSession(false); // ������������ ���� �� �����
        }
    }
}
