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

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y >= 6.6f || transform.position.y <= -6.6f)
        {

            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }

            Destroy(this.gameObject);   
        }
    }

    void FixedUpdate()
    {
        if (this.transform.parent == _enemyLaser.transform)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.down * _speed * Time.fixedDeltaTime);
        }
        else
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.up * _speed * Time.fixedDeltaTime);
        }
    }
}
