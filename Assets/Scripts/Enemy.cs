using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    [SerializeField]
    private float _yEnemyLaserPosition;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(Shoot());
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

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.7f));

        int shootingEnemyID = Random.Range(1, 4);

        if (shootingEnemyID == 3)
        {
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab,
                new Vector3(transform.position.x, transform.position.y + _yEnemyLaserPosition, 0),
                Quaternion.identity);

            var lasers = enemyLaser.GetComponentsInChildren<Laser>();

            foreach (var laser in lasers)
            {
                laser.AssignEnemyLaser();
            }
        }
    }

    public void Damage()
    {
        Destroy(this.gameObject);
    }
}
