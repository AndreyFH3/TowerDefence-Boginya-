using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonHandler : MonoBehaviour
{
    [SerializeField] private BuildingCreator creator;
    [SerializeField] private BuildingObectbase item;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtotnClicked);
    }

    private void ButtotnClicked()
    {
        if (creator.GetSelectedObstacle != item)
        {
            creator.SelectObject(item);
        }
        else if (creator.IsTileSelected)
        {
            creator.Deselect();
        }
    }
}