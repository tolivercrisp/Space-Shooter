using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // new package to include to access unity Text object;

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

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _speed = Random.Range(8.0f, 10.0f);
        transform.position = new Vector3(Random.Range(-13.0f, 13.0f), Random.Range(12.0f, 15.0f), 0);
        _audioSource = GetComponent<AudioSource>();

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            DestroyChildren();
            Destroy(this.gameObject, 1.2f);
            _audioSource.Play();
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
            DestroyChildren();
            Destroy(this.gameObject, 1.2f);
            _audioSource.Play();

        }
    }

    public void DestroyChildren()
    {
        // Destroy the thrusters too
        _anim.SetTrigger("OnEnemyDeath");
        int nbChildren = this.transform.childCount;

        for (int i = nbChildren - 1; i >= 0; i--)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }











}
