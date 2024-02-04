
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
    [SerializeField] private SpriteRenderer _SpriteIndicator;

    private CharacterLeader _Leader;
    private ObstacleConfig  _ObstacleConfig;

    #endregion Data

    #region Construct

    [Inject]
    public void Constructor(CharacterLeader _Leader, ObstacleConfig _ObstacleConfig)
    {
        this._Leader = _Leader;
        this._ObstacleConfig = _ObstacleConfig;
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
                _Leader.RemoveCharacter(_Leader.AllCharacter.Count / _Size);
                break;
        }

        Dispose();
    }

    public void Dispose()
    {
        //_Collider.enabled = true;
    }

    public void UpdateText()
    {
        switch (_eMaths)
        {
            case eMaths.Additive:
                _Text.text = "+"+_Size;
                _Text.fontSharedMaterial = _ObstacleConfig.PositiveTextMat;
                _SpriteIndicator.color = _ObstacleConfig.PositiveColor;
                break;
            case eMaths.Multiple:
                _Text.text = "X" + _Size;
                _Text.fontSharedMaterial = _ObstacleConfig.PositiveTextMat;
                _SpriteIndicator.color = _ObstacleConfig.PositiveColor;
                break;
            case eMaths.Subtract:
                _Text.text = "-" + _Size;
                _Text.fontSharedMaterial = _ObstacleConfig.NegativeTextMat;
                _SpriteIndicator.color = _ObstacleConfig.NegativeColor;
                break;
            case eMaths.Divide:
                _Text.text = "\u00F7" + _Size;
                _Text.fontSharedMaterial = _ObstacleConfig.NegativeTextMat;
                _SpriteIndicator.color = _ObstacleConfig.NegativeColor;
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

