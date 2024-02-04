using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Screen_LevelFinish : MonoBehaviour, IMenuView
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
