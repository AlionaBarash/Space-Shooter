using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _explosionPrefab;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _collider = GetComponent<Collider2D>();
        _collider.enabled = true;
    }

    void Update()
    {
        if (transform.position.y <= -6.6f)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector2.down * _speed * Time.fixedDeltaTime);
    }

    public virtual void Damage()
    {
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;

        _collider.enabled = false;

        Destroy(this.gameObject, 1.5f);

        Destroy(explosion, 2.5f);
    }
}
