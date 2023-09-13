using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
         _rigidbody = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        if (transform.position.y <= -6.6)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector2.down * _speed * Time.fixedDeltaTime);
    }
}
