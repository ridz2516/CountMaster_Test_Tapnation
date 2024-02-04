
using TMPro;
using UnityEngine;

public class BubbleText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _CharacterText;

    public void UpdateText(int _Amount)
    {
        _CharacterText.text = ""+_Amount;
    }
}
