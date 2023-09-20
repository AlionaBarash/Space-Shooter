using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IShootable
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _xRightLimit, _xLeftLimit;
    [SerializeField]
    private float _yUpperLimit, _yLowerLimit;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _yLaserPosition;
    [SerializeField]
    private float _reloadTime;

    private Rigidbody2D _rigidbody;
    private Vector2 _input;
    private Vector2 _e;
    private float _currentTime;
    private int _health = 3;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
       
    }

    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _speed;

        LimitPlayerMovement();

        _currentTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && _currentTime <= 0)
        {
            Shoot();
            _currentTime = _reloadTime;
        }
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _input * Time.fixedDeltaTime);
    }

    private void LimitPlayerMovement()
    {
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

    public void Shoot()
    {
        Instantiate(_laserPrefab, 
            new Vector3(transform.position.x, transform.position.y + _yLaserPosition, 0), 
            Quaternion.identity);
    }

    public void Damage()
    {
        _health--;

        if (_health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
