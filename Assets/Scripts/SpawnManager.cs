using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // variables
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _speedBoostPrefab;
    [SerializeField]
    private GameObject _overshieldPrefab;
    [SerializeField]
    private GameObject[] _powerupsArray;

    private int randomPowerup;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        

    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        while (_stopSpawning == false)
        {
            Vector3 positionToSpawn = new Vector3(Random.Range(-12.0f, 12.0f), 20.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.0f);
        }
    }

    // Spawn a random Powerup
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        while (_stopSpawning == false)
        {
            Vector3 positionToSpawn = new Vector3(Random.Range(-12.0f, 12.0f), 13.0f, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupsArray[randomPowerup], positionToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 10));

        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

}
