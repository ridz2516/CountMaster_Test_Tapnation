
using UnityEngine;
using UnityEngine.UI;

public class Level_Complete : MonoBehaviour, IMenuView
{
    public Button RestartButton;

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
