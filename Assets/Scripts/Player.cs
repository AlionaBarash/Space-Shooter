using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Movement, IDamageable
{
    [Header("Movement Limits")]
    [SerializeField]
    private float _xRightLimit;
    [SerializeField]
    private float _yUpperLimit, _yLowerLimit;

    [Header("Laser")]
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _yLaserPosition;
    [SerializeField]
    private float _reloadTime;

    [Header("Boosters")]
    [SerializeField]
    private float _boostedSpeed;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _yTripleShotPosition;

    private List<IPowerUp> _powerUps;

    private Vector2 _input;
    private int _health = 3;
    private float _canFire;
    private bool _pauseReload;

    public static Action onBoostDeactivation;
    public static Action<int> onPlayerDamage;

    void Start()
    {
        _powerUps = new List<IPowerUp>();
    }

    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        LimitPlayerMovement();

        _canFire -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && _canFire <= 0)
        {
            Shoot();

            if (!_pauseReload)
            {
                _canFire = _reloadTime;
            }
        }

        StopBoosterImpact();
    }

    void FixedUpdate()
    {
        if (BoosterImpact<SpeedBoost>())
        {
            MoveWithSpeedBoost();
        }
        else
        {
            Move();
        }
    }

    protected override void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _input * _speed * Time.fixedDeltaTime);
    }

    void MoveWithSpeedBoost()
    {
        _rigidbody.MovePosition(_rigidbody.position + _input * _boostedSpeed * Time.fixedDeltaTime);
    }

    private void LimitPlayerMovement()
    {
        if (transform.position.x > _xRightLimit)
        {
            transform.position = new Vector2(-_xRightLimit, transform.position.y);
        }
        else if (transform.position.x < -_xRightLimit)
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
        if (BoosterImpact<TripleShotBoost>())
        {
            Instantiate(_tripleShotPrefab, 
                new Vector3(transform.position.x, transform.position.y + _yTripleShotPosition, 0), 
                Quaternion.identity);

            _pauseReload = true;
        }
        else
        {
            Instantiate(_laserPrefab,
                new Vector3(transform.position.x, transform.position.y + _yLaserPosition, 0),
                Quaternion.identity);

            _pauseReload = false;
        }
    }

    public void Damage()
    {
        if (BoosterImpact<ShieldBoost>())
        {
            onBoostDeactivation?.Invoke();        
        }
        else
        {
            _health--;

            onPlayerDamage?.Invoke(_health);

            if (_health == 0)
            {
                Destroy(this.gameObject);
            }
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

        IPowerUp powerUp = other.GetComponent<IPowerUp>();
        if (powerUp != null)
        {
            // ---> если добавлять проверку на дублирование, то по Name

            _powerUps.Add(powerUp);

            powerUp.ActivateBoost();

            powerUp.AfterCollectingBoost();
        }
    }

    private bool BoosterImpact<T>() where T : IPowerUp
    {
        foreach (var booster in _powerUps)
        {
            if (booster.GetType() == typeof(T))
            {
                return true;
            }
        }

        return false;
    }

    private void StopBoosterImpact()
    {
        foreach (var booster in _powerUps)
        {
            if (!booster.IsBoostActive)
            {
                _powerUps.Remove(booster);

                break;
            }
        }
    }
}
