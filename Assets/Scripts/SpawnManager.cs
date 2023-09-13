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
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _delayTime;
    [SerializeField]
    private float _ySpawnPosition;
    [SerializeField]
    private float[] _xSpawnPositions;
    [SerializeField]
    private Wave[] _waves;

    private HashSet<float> _availableSpawnPositions;

    void Start()
    {
        _availableSpawnPositions = new HashSet<float>();

        StartCoroutine(SelectWave());
    }

    private void SpawnEnemies(float xSpawnPosition)
    {
        GameObject enemy = Instantiate(_enemyPrefab, new Vector3(xSpawnPosition, _ySpawnPosition, 0), Quaternion.identity);

        enemy.transform.parent = _enemyContainer.transform;
    }

    IEnumerator SelectWave()
    {
        //will be condition
        while (true)
        {
            _availableSpawnPositions.AddRange(_xSpawnPositions);

            int randomWave = Random.Range(0, _waves.Length);

            for (int i = 0; i < _waves[randomWave].enemyAmount; i++)
            {
                int randomIndex = Random.Range(0, _availableSpawnPositions.Count);
                float xSpawnPosition = _availableSpawnPositions.ElementAt(randomIndex);
                SpawnEnemies(xSpawnPosition);
                _availableSpawnPositions.Remove(xSpawnPosition);
            }

            yield return new WaitForSeconds(_delayTime);
        }
    }



}
