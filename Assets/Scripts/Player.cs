﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // variables
    // 1) public or private reference
    // 2) data types (int, float, bool, string)
    // 3) name of the variable
    // optional -- value assigned

    [SerializeField]
    private float _speed = 14.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    // Laser Shooting
    [SerializeField]
    private float _fireRate = 0.001f;
    [SerializeField]
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;


    // Start is called before the first frame update -----------------------------------------
    void Start()
    {
        // take the current position --> assign new position (0, 0, 0)
        transform.position = new Vector3(0, -4.0f, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL (See 'Player' Script...");

        }
    }

    // Update is called once per frame -------------------------------------------------------
    void Update()
    {
        CalculateMovement();

        // if i press space -> spawn a laser
        // i removed the fire rate from if statement (&& Time.time > _canFire)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireLaser();
        }
          



    }

    // Function for Movement code
    void CalculateMovement()
    {
        // Press either A or D, move Player left and right at "speed" variable value
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);

        // Press either W or A, move Player up and down at "speed" variable value
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);


        // Up and Down Boundary Limits
        // --- Math.f(Clam) is the value of a range
        float verticalRange = Mathf.Clamp(transform.position.y, -7.75f, 7.75f);
        transform.position = new Vector3(transform.position.x, verticalRange, 0);

        // Left and Right Boundary WRAP
        if (transform.position.x >= 16.2f)
        {
            transform.position = new Vector3(-16.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -16.2f)
        {
            transform.position = new Vector3(16.2f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {
       _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;

        if(_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
            Debug.Log("Game Over! [Player.cs script]");
       
        }
    }
}
