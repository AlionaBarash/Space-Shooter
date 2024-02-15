using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedEnemy : Enemy
{
    private Animator _animator;
    private int _health = 2;

    void Start()
    {
        _damagePoints = 20;

        _animator = GetComponent<Animator>();
    }

    public override void Damage()
    {
        _health--;

        _animator.SetInteger("FirstDamage", _health);

        if (_health == 0)
        {
            base.Damage();
        }
    }
}
