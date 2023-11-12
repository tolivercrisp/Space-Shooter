using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _rotateSpeed = 19.0f;
    [SerializeField]
    private GameObject _asteroidPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;




    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-13.0f, 13.0f), Random.Range(12.0f, 15.0f), 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("_spawnManager is NULL (SpawnManager.cs)");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Move down
        // transform.Translate(Vector3.down * Time.deltaTime * _speed);
        // Rotate Asteroid 3m/s
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);
        }
    }
}
