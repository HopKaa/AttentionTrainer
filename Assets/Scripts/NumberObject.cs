/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberObject : MonoBehaviour
{
    public Text numberText; // ��������� Text ��� ����������� �����
    private int number; // �������� �����
    public GameSessionManager gameSessionManager;

    public void GenerateNumber()
    {
        number = Random.Range(1, 10);
        numberText.text = number.ToString();
    }

    void Start()
    {
        GenerateNumber();
    }
    public void OnNumberClick()
    {
        gameSessionManager.CheckClick(number);
    }
}*/
