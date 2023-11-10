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
    private float _speedMultiplier = 1.5f;

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
        if(transform.childCount < 1)
        {
            _lives--;
            Debug.Log("Remaining Lives: " + _lives);
        }

        if(_lives < 1)
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
            Destroy(transform.GetChild(0).gameObject);
        }
    }


}
