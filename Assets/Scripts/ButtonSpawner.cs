using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawner : MonoBehaviour
{
    public GameObject _buttonPrefab;
    public Slider _slider;
    public int _minNumber = 1;
    public int _maxNumber = 9;
    public Transform _panelTransform;

    private void Start()
    {
        SpawnButtons();
    }

    private void SpawnButtons()
    {
        int numberOfButtons = (int)_slider.value;

        if (numberOfButtons < _minNumber)
        {
            numberOfButtons = _minNumber;
        }
        else if (numberOfButtons > _maxNumber)
        {
            numberOfButtons = _maxNumber;
        }

        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject buttonObject = Instantiate(_buttonPrefab);
            buttonObject.transform.SetParent(_panelTransform);

            RectTransform panelRect = _panelTransform.GetComponent<RectTransform>();
            Vector2 randomPosition = GetRandomPositionInPanel(panelRect);

            buttonObject.transform.localPosition = randomPosition;
            buttonObject.GetComponent<Button>().onClick.AddListener(() => DestroyButton(buttonObject));
        }
    }

    private Vector2 GetRandomPositionInPanel(RectTransform panelRect)
    {
        float minX = panelRect.rect.xMin;
        float maxX = panelRect.rect.xMax;
        float minY = panelRect.rect.yMin;
        float maxY = panelRect.rect.yMax;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }

    private void DestroyButton(GameObject buttonObject)
    {
        Destroy(buttonObject);
    }
}