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
    private WaveDifficulty[] _waveDifficulties;

    private HashSet<float> _availableSpawnPositions;

    private bool stopSpawning;

    void Start()
    {
        _availableSpawnPositions = new HashSet<float>();

        foreach (var waveDifficulty in _waveDifficulties)
        {
            waveDifficulty.ResetWeightValues();
        }

        StartCoroutine(RunWaves());

        HUD_UI.onWeightChange += StartWeightChangeProcess;
        Player.onPlayerDeath += StopSpawningWaves;
    }

    private void SpawnEnemies(float xSpawnPosition, GameObject enemyPrefab, bool isShootingEnemy, bool isExplosingEnemy)
    {
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(xSpawnPosition, _ySpawnPosition, 0), Quaternion.identity);
        var enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.AssignEnemiesFeatures(isShootingEnemy, isExplosingEnemy);

        enemy.transform.parent = _enemyContainer.transform;
    }

    private void SpawnBoosters(float xSpawnPosition, GameObject powerupPrefab)
    {
        Instantiate(powerupPrefab, new Vector3(xSpawnPosition, _ySpawnPosition, 0), Quaternion.identity);
    }

    IEnumerator RunWaves()
    {
        while (!stopSpawning)
        {
            _availableSpawnPositions.AddRange(_xSpawnPositions);

            int difficultyIndex = SelectWaveDifficulty();

            int randomWaveIndex = Random.Range(0, _waveDifficulties[difficultyIndex].waves.Length);

            // один прогон цикла - один заспавненый элемент
            for (int i = 0; i < _waveDifficulties[difficultyIndex].waves[randomWaveIndex].enemies.Length; i++)
            {
                int randomIndex = Random.Range(0, _availableSpawnPositions.Count);

                float xSpawnPosition = _availableSpawnPositions.ElementAt(randomIndex);
                GameObject enemyPref = _waveDifficulties[difficultyIndex].waves[randomWaveIndex].enemies[i].enemyPrefab;
                bool isShootingEnemy = _waveDifficulties[difficultyIndex].waves[randomWaveIndex].enemies[i].isShooting;
                bool isExplosingEnemy = _waveDifficulties[difficultyIndex].waves[randomWaveIndex].enemies[i].isExplosing;

                SpawnEnemies(xSpawnPosition, enemyPref, isShootingEnemy, isExplosingEnemy);

                _availableSpawnPositions.Remove(xSpawnPosition);
            }

            for (int i = 0; i < _waveDifficulties[difficultyIndex].waves[randomWaveIndex].powerups.Length; i++)
            {
                int randomIndex = Random.Range(0, _availableSpawnPositions.Count);

                float xSpawnPosition = _availableSpawnPositions.ElementAt(randomIndex);
                GameObject boosterPref = _waveDifficulties[difficultyIndex].waves[randomWaveIndex].powerups[i];

                SpawnBoosters(xSpawnPosition, boosterPref);

                _availableSpawnPositions.Remove(xSpawnPosition);
            }

            yield return new WaitForSeconds(_delayTime);
        }
    }

    private int SelectWaveDifficulty()
    {
        int randomWeightValue = Random.Range(1, 11);

        int processedWeight = 0;

        for (int i = 0; i < _waveDifficulties.Length; i++)
        {
            processedWeight += _waveDifficulties[i].Weight;

            if (randomWeightValue <= processedWeight)
            {
                return i;
            }
        }

        return -1;
    }

    private void StartWeightChangeProcess()
    {
        foreach (var waveDifficulty in _waveDifficulties)
        {
            waveDifficulty.ChangeWeight();
        }
    }

    private void StopSpawningWaves()
    {
        stopSpawning = true;
    }

    void OnDestroy()
    {
        HUD_UI.onWeightChange -= StartWeightChangeProcess;
        Player.onPlayerDeath -= StopSpawningWaves;
    }
}
