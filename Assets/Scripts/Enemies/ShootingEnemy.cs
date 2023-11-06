using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    [SerializeField]
    private float _yEnemyLaserPosition; //?

    void Start()
    {
        StartCoroutine(Shoot());
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
}
