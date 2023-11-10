using System.Collections;
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
    private float _speed = 15.0f;
    [SerializeField]
    private float _boostedSpeed;

    private float _fireRate = 0.001f;
    [SerializeField]
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _bgHorizontalSpeed = -0.5f;
    [SerializeField]
    private float _bgVerticalSpeed = 0.5f;

    // Bool variable for Powerups
    public bool _isTripleShotActive = false;
    public bool _isSpeedBoostActive = false;

    // Prefabs
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _speedBoostPrefab;

    // Objects
    private SpawnManager _spawnManager;
    private Background _Background;



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
        BackgroundMovement();

        // if i press mouse -> spawn a laser
        // i removed the fire rate from if statement (&& Time.time > _canFire)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireLaser();
        }

       


    }

    // Function for Movement code
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // if SpeedBoost is enabled, move faster
        if (_isSpeedBoostActive == true)
        {
            _boostedSpeed = _speed * 1.5f;
            transform.Translate(direction * _boostedSpeed * Time.deltaTime);
        } else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -7.75f, 7.75f));


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

    void BackgroundMovement()
    {
        // import the Background object
        _Background = GameObject.Find("Background").GetComponent<Background>();
        // access transform position
        float horizontalInput = Input.GetAxis("Horizontal");
        _Background.transform.Translate(Vector3.right * horizontalInput * _bgHorizontalSpeed * Time.deltaTime);
        float verticalInput = Input.GetAxis("Vertical");
        _Background.transform.Translate(Vector3.up * verticalInput * _bgVerticalSpeed * Time.deltaTime);

        // HEY! Eventually the player will scoot themselves off the map bc of sideways 
    }

    public void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        } else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
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

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        Debug.Log("TripleShot = ON (Player.cs)");
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        Debug.Log("TripleShot Coroutine Started...");
        while (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
            Debug.Log("TripleShot = OFF (Player.cs");
        }
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        Debug.Log("Speed Powerup = ON (Player.cs)");
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        Debug.Log("SpeedBoost Coroutine Started...");
        while(_isSpeedBoostActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedBoostActive = false;
            Debug.Log("Speed Boost = OFF (Player.cs)");
        }
    }
}
