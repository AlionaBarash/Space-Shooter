using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _delayTime;
    [SerializeField]
    private float _ySpawnPosition;
    [SerializeField]
    private float[] _xSpawnPositions;
    [SerializeField]
    private Wave[] _waves;

    private HashSet<float> _availableSpawnPositions;

    private bool test = true;

    void Start()
    {
        _availableSpawnPositions = new HashSet<float>();

        StartCoroutine(SelectWave());
    }

    private void SpawnEnemies(float xSpawnPosition, GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(xSpawnPosition, _ySpawnPosition, 0), Quaternion.identity);

        enemy.transform.parent = _enemyContainer.transform;
    }

    private void SpawnBoosters(float xSpawnPosition, GameObject powerupPrefab)
    {
        Instantiate(powerupPrefab, new Vector3(xSpawnPosition, _ySpawnPosition, 0), Quaternion.identity);
    }

    IEnumerator SelectWave()
    {
        //will be condition
        while (test)
        {
            _availableSpawnPositions.AddRange(_xSpawnPositions);

            int randomWave = Random.Range(0, _waves.Length);

            // один прогон цикла - один заспавненый элемент
            for (int i = 0; i < _waves[randomWave].enemies.Length; i++)
            {
                int randomIndex = Random.Range(0, _availableSpawnPositions.Count);

                float xSpawnPosition = _availableSpawnPositions.ElementAt(randomIndex);

                SpawnEnemies(xSpawnPosition, _waves[randomWave].enemies[i]);

                _availableSpawnPositions.Remove(xSpawnPosition);
            }

            for (int i = 0; i < _waves[randomWave].powerups.Length; i++)
            {
                int randomIndex = Random.Range(0, _availableSpawnPositions.Count);

                float xSpawnPosition = _availableSpawnPositions.ElementAt(randomIndex);

                SpawnBoosters(xSpawnPosition, _waves[randomWave].powerups[i]);

                _availableSpawnPositions.Remove(xSpawnPosition);
            }

            yield return new WaitForSeconds(_delayTime);
        }
    }



}
