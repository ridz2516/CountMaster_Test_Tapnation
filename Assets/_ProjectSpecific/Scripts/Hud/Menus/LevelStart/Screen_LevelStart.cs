using UnityEngine;
using UnityEngine.UI;

public class Screen_LevelStart : MonoBehaviour, IMenuView
{
    public ExtendedButton PlayButton;

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
