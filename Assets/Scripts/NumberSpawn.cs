using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSpawn : MonoBehaviour
{
    public GameObject numberPrefab; // Ссылка на префаб объекта-числа
    public Transform spawnPoint; // Позиция, где будут появляться числа
    public int numberOfNumbersToSpawn; // Количество чисел, которые нужно создать
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
