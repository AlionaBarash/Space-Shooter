using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : Movement
{
    private bool _isEnemyShooting;
    private bool _ignoreShooter = true;

    void Update()
    {
        DestroyAfterPassingLimits();
    }

    void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        if (_isEnemyShooting)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.down * _speed * Time.fixedDeltaTime);
        }
        else
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.up * _speed * Time.fixedDeltaTime);
        }
    }

    void DestroyLaser()
    {
        Destroy(this.gameObject);
    }

    protected override void DestroyAfterPassingLimits()
    {
        if (transform.position.y >= 6.6f || transform.position.y <= -6.6f)
        {
            DestroyLaser();

            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyShooting = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_ignoreShooter)
        {
            _ignoreShooter = false;
        }
        else
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage();

                DestroyLaser();
            }
        }
    }
}
