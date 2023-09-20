using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IShootable
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    [SerializeField]
    private float _yEnemyLaserPosition;

    private Rigidbody2D _rigidbody;
    private float _delayTime;
    private float _currentTime;
    private bool _isShooting;
    private int _enemyShootingRandomID;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _delayTime = Random.Range(0.5f, 1f);

        _enemyShootingRandomID = Random.Range(1, 4);
    }

    void Update()
    {
        if (!_isShooting)
        {
            _delayTime -= Time.deltaTime;
        }

        if (_delayTime <= 0 && !_isShooting && _enemyShootingRandomID == 3)
        {
            Shoot();
            _isShooting = true;
        }

        if (transform.position.y <= -6.6f)
        {
            Destroy(this.gameObject);
        }     
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector2.down * _speed * Time.fixedDeltaTime);
    }

    public void Shoot()
    {
        Instantiate(_enemyLaserPrefab,
            new Vector3(transform.position.x, transform.position.y + _yEnemyLaserPosition, 0),
            Quaternion.identity);
    }

    public void Damage()
    {
        Destroy(this.gameObject);
    }
}
