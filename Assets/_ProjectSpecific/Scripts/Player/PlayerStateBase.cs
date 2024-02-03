using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase
{

    protected Player _Player;

    public void SetOwner(Player _Player)
    {
        this._Player = _Player;
    }
    public virtual void Start() {}
    public virtual void Update() { }

    public virtual void Disposable() { }

    public virtual void OnTriggerEnter(Collider collider) { }
}
