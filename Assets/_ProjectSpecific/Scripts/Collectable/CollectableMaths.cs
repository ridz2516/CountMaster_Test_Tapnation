
using TMPro;
using UnityEngine;
using Zenject;

public class CollectableMaths : MonoBehaviour, ICollectable
{
    #region Data

    [SerializeField] private eMaths     _eMaths;
    [SerializeField,Min(1)] private int _Size;
    [SerializeField] private GameObject _ViewObject;
    [SerializeField] private Collider   _Collider;
    [SerializeField] private TextMeshPro   _Text;

    private CharacterLeader _Leader;

    #endregion Data

    #region Construct

    [Inject]
    public void Constructor(CharacterLeader _Leader)
    {
        this._Leader = _Leader;
    }

    #endregion Construct


    private void Start()
    {
        _ViewObject.SetActive(true);
        _Collider.enabled = true;
        UpdateText();
    }

    public void Collect()
    {
        switch (_eMaths)
        {
            case eMaths.Additive:
                _Leader.AddCharacter(_Size);
                break;
            case eMaths.Multiple:
                _Leader.AddCharacter(_Leader.AllCharacter.Count * _Size);
                break;
            case eMaths.Subtract:
                _Leader.RemoveCharacter(_Size);
                break;
            case eMaths.Divide:
                _Leader.AddCharacter(_Leader.AllCharacter.Count / _Size);
                break;
        }

        Dispose();
    }

    public void Dispose()
    {
        _Collider.enabled = true;
    }

    public void UpdateText()
    {
        switch (_eMaths)
        {
            case eMaths.Additive:
                _Text.text = "+"+_Size;
                break;
            case eMaths.Multiple:
                _Text.text = "X" + _Size;
                break;
            case eMaths.Subtract:
                _Text.text = "-" + _Size;
                break;
            case eMaths.Divide:
                _Text.text = "\u00F7" + _Size;
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(nameof(eTags.Player)))
        {
            Collect();
        }
    }
}

public enum eTags
{
    Player,
    Enemy,
    Finish
}
