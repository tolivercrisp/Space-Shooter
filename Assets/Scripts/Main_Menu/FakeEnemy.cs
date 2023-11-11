using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FakeEnemy : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _fakeEnemy;


    // This is basically just Enemy movement and respawning at random locations, copied from Enemy.cs



    void Start()
    {
        _speed = 2.0f;
    }

    void Update()
    {
        //Move enemy down at range of 1-3 meters/s
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        //if bottom of screen
        //respawn at top with a new random x position

        if (transform.position.y < -10.0f)
        {
            float randomX = Random.Range(-13.0f, 13.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

}
