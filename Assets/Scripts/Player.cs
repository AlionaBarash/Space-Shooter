using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _xRightLimit, _xLeftLimit;
    [SerializeField]
    private float _yUpperLimit, _yLowerLimit;

    private Rigidbody2D _rigidbody;
    private Vector2 _input;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _speed;


        if (transform.position.x > _xRightLimit)
        {
            transform.position = new Vector2(_xLeftLimit, transform.position.y);
        }
        else if (transform.position.x < _xLeftLimit)
        {
            transform.position = new Vector2(_xRightLimit, transform.position.y);
        }

        if (transform.position.y > _yUpperLimit || transform.position.y < _yLowerLimit)
        {
            transform.position = new Vector2(transform.position.x,
                Mathf.Clamp(transform.position.y, _yLowerLimit, _yUpperLimit));
        }
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _input * Time.fixedDeltaTime);
    }
}
