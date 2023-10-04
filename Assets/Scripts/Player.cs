using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _boostedSpeed;
    [SerializeField]
    private float _xRightLimit, _xLeftLimit;
    [SerializeField]
    private float _yUpperLimit, _yLowerLimit;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _yLaserPosition;
    [SerializeField]
    private float _reloadTime;
    [SerializeField]
    private GameObject _playerShield;

    private Rigidbody2D _rigidbody;
    private Vector2 _input;
    private float _canFire;
    private int _health = 3;
    private bool _isSpeedBoostActive;
    private bool _isTripleShotActive;
    private float _speedBoostDuration = 5f;
    private float _tripleShotBoostDuration = 5f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        LimitPlayerMovement();

        _canFire -= Time.deltaTime;

        if (_isTripleShotActive) 
        {
            _tripleShotBoostDuration -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _canFire <= 0)
        {
            Shoot();

            if (!_isTripleShotActive)
            {
                _canFire = _reloadTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (_isSpeedBoostActive)
        {
            if (_speedBoostDuration > 0)
            {
                _rigidbody.MovePosition(_rigidbody.position + _input * _boostedSpeed * Time.fixedDeltaTime);

                _speedBoostDuration -= Time.fixedDeltaTime;
            }
            else
            {
                _speedBoostDuration = ResetBoostDurationValue();
                _isSpeedBoostActive = false;
            }
        }
        else
        {
            _rigidbody.MovePosition(_rigidbody.position + _input * _speed * Time.fixedDeltaTime);
        }    
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

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
    }

    public void Shoot()
    {
        if (_isTripleShotActive)
        {
            if (_tripleShotBoostDuration > 0)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                _isTripleShotActive = false;
                _tripleShotBoostDuration = ResetBoostDurationValue();
            }
        }
        else
        {
            Instantiate(_laserPrefab,
                new Vector3(transform.position.x, transform.position.y + _yLaserPosition, 0),
                Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Damage();

            Damage();
        }
    }

    public void ActivateSpeedBoost()
    {
        _isSpeedBoostActive = true;
    }

    private float ResetBoostDurationValue()
    {
        return 5;
    }

    public void ActivateShield()
    {
        _playerShield.SetActive(true);
    }

    public void Damage()
    {
        if (!_playerShield.activeSelf)
        {
            _health--;

            if (_health == 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            _playerShield.SetActive(false);
        }
    }  
}
