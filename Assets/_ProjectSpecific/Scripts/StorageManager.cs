
using UnityEngine;

public class StorageManager
{
    public int CurrentLevel { get => PlayerPrefs.GetInt(nameof(CurrentLevel),1); set => PlayerPrefs.SetInt(nameof(CurrentLevel), value); }
}
