using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, ICharacter
{
    [SerializeField] protected Animator _Animator;

    public virtual void SetAnimation(eCharacterAnimation _eAnim)
    {
        _Animator.SetTrigger(_eAnim.ToString());
    }

    public abstract void DestroyCharacter();
}
