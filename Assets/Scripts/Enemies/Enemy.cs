using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Movement, IDamageable
{
    [SerializeField]
    private GameObject _explosionPrefab;

    private Collider2D _collider;

    protected bool _isSelfDestroyedEnemy;

    public static Action onEnemyDamage;

    protected virtual void OnEnable()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;
    }
    
    void Update()
    {
        base.DestroyAfterPassingLimits();
    }

    void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector2.down * _speed * Time.fixedDeltaTime);
    }

    public virtual void Damage()
    {
        if (!_isSelfDestroyedEnemy)
        {
            onEnemyDamage?.Invoke();
        }

        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;

        _collider.enabled = false;

        Destroy(this.gameObject, 1.5f);

        Destroy(explosion, 2.5f);
    }
}
