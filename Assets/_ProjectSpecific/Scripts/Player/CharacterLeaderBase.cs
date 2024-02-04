using UnityEngine;

public abstract class CharacterLeaderBase : MonoBehaviour
{
    public abstract void AddCharacter(int _Amount);
    public abstract void AttackCharacter(Transform _Target);
    public virtual void RemoveCharacter(int _Amount) { }

}
