using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables ---
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _speed;


    // Start is called before the first frame update
    void Start()
    {
        _speed = Random.Range(1.0f, 3.0f);
        transform.position = new Vector3(Random.Range(-13.0f, 13.0f), Random.Range(11.0f, 15.0f), 0);
    }


    // Update is called once per frame
    void Update()
    {
        //Move enemy down at range of 1-3 meters/s
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        //if bottom of screen
        //respawn at top with a new random x position

        if(transform.position.y < -10.0f)
        {
            float randomX = Random.Range(-13.0f, 13.0f);
            transform.position = new Vector3(randomX, 7, 0);
        } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }











}
