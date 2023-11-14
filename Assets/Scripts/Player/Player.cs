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
    private float _speedMultiplier = 1.5f;

    private float _fireRate = 0.001f;
    [SerializeField]
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;

    // Bool variable for Powerups
    public bool _isTripleShotActive = false;
    public bool _isSpeedBoostActive = false;
    public bool _isOvershieldActive = false;

    // Prefabs
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _speedBoostPrefab;
    [SerializeField]
    private GameObject _overshieldPrefab;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;

    // Objects
    private SpawnManager _spawnManager;
    private Background _Background;

    // UI
    [SerializeField]
    private int _score;

    [SerializeField]
    private AudioClip _laserAudioClip;
    [SerializeField]
    private AudioSource _audioSource;

    // // Handle (stores the reference for another object, better performance,
    // not calling "GetComponent" all the time))
    private UIManager _uiManager;




    // Start is called before the first frame update -----------------------------------------
    void Start()
    {
        // take the current position --> assign new position (0, 0, 0)
        transform.position = new Vector3(0, -4.0f, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("*** Spawn Manager is NULL (See 'Player' Script...");

        }
        if (_uiManager == null)
        {
            Debug.LogError("*** UI Manager is NULL! (Player.cs)");
        }

        if (_audioSource == null)
        {
            Debug.LogError("*** AudioSource(Player) is NULL! (Player.cs)");
        } else
        {
            _audioSource.clip = _laserAudioClip;
        }

        _leftEngine.SetActive(false);
        _rightEngine.SetActive(false);
    }

    // Update is called once per frame -------------------------------------------------------
    void Update()
    {
        CalculateMovement();
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

        transform.Translate(direction * _speed * Time.deltaTime);

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

        _audioSource.Play();
    }

    public void Damage()
    {
        if(transform.childCount == 3)
        {
            _lives--;
            Debug.Log("Remaining Lives: " + _lives);
        }

        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        } else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Destroy(gameObject);
            Debug.Log("Game Over! [Player.cs script]");
       
        }
    }

    // TripleShot
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        Debug.Log("TripleShot = ON (Player.cs)");
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        while (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }
    }

    //SpeedBoost
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        Debug.Log("Speed Powerup = ON (Player.cs)");
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        while(_isSpeedBoostActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedBoostActive = false;
            _speed /= _speedMultiplier;
        }
    }

    // Overshield
    public void OvershieldActive()
    {
        _isOvershieldActive = true;
        // if we get the power up, make the Player the parent of OvershieldBubble
        GameObject newShield = Instantiate(_overshieldPrefab, transform.position, Quaternion.identity);
        newShield.transform.parent = transform;
        StartCoroutine(OvershieldPowerDownRoutine());
    }

    public IEnumerator OvershieldPowerDownRoutine()
    {
        while(_isOvershieldActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isOvershieldActive = false;
            Destroy(transform.GetChild(3).gameObject);
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    // method to add 10 to the score!
    // Communicate with the UI to update the score! (update in UIManager.cs)


}
