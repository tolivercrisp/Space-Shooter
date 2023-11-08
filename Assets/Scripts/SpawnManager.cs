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

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn game objects every 5 seconds
    // create a coroutine of type IEnumerator -- Yield Events
    //while loop

    IEnumerator SpawnRoutine()
    {
        
        while (_stopSpawning == false)
        {
            Vector3 positionToSpawn = new Vector3(Random.Range(-13.0f, 13.0f), Random.Range(-12.0f, 12.0f), 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.0f);

        }

        
        //while loop (infinite loop)
            // instantiate enemy prefab
            // yield wait for 5 seconds
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

}
