using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Data

    [SerializeField] private float  _Speed = 1f;

    private Transform _Target;
    private float _CurrentSpeed = 0;

    #endregion Data

    private void Start()
    {

        StartMovement();
    }

    private void FixedUpdate()
    {
        if(_Target != null)
        {
            var direction = _Target.position - transform.position;
            Move(direction.normalized);
        }
    }

    public void SetTarget(Transform _Target)
    {
        this._Target = _Target;

        StartMovement();
    }

    public void Move(Vector3 _Direction)
    {
        transform.position = Vector3.MoveTowards(transform.position, _Target.position, _CurrentSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        _Speed = 0;
    }

    public void StartMovement()
    {
        _CurrentSpeed = _Speed;
    }

}
