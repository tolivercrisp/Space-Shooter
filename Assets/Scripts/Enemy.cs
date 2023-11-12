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

    // Handle (references another object, stores it, better performance)
    private Player _player;

    // Animator handle
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _speed = Random.Range(7.0f, 11.0f);
        transform.position = new Vector3(Random.Range(-13.0f, 13.0f), Random.Range(12.0f, 15.0f), 0);

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }
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
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 1.2f);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            // Add 10,000 to score
            if(_player != null)
            {
                _player.AddScore(10000);
            }
            GetComponent<BoxCollider2D>().enabled = false;
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject, 1.2f);

        }

        
    }











}
