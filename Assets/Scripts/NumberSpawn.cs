using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSpawn : MonoBehaviour
{
    public GameObject numberPrefab; // ������ �� ������ �������-�����
    public Transform spawnPoint; // �������, ��� ����� ���������� �����
    public int numberOfNumbersToSpawn; // ���������� �����, ������� ����� �������
    public GameSessionManager gameSessionManager;

    void Start()
    {
        SpawnNumbers();
    }

    void SpawnNumbers()
    {
        for (int i = 0; i < numberOfNumbersToSpawn; i++)
        {
            GameObject numberObject = Instantiate(numberPrefab, spawnPoint.position, Quaternion.identity);
            NumberObject numberScript = numberObject.GetComponent<NumberObject>();
            numberScript.gameSessionManager = gameSessionManager;
            numberScript.GenerateNumber();
        }
    }
}
