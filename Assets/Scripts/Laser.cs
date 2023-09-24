using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _enemyLaser;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private bool _isEnemyShooting;
    private bool _ignoreShooter = true;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _collider = GetComponent<Collider2D>();
    }

    void Update()
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

    void FixedUpdate()
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
