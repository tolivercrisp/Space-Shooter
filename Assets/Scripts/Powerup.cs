using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Variables
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]  // Powerup ID's (0 = TripleShot, 1 = Speed, 2 = Shield)
    private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3m/s
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // when we leave the screen, destroy this object
        if (transform.position.y <= -13.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                // switch statement
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        Debug.Log("Picked up TRIPLE SHOT!");
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        Debug.Log("Picked up SPEED BOOST!");
                        break;
                    case 2:
                        // player.OvershieldActive();
                        Debug.Log("Picked up OVERSHIELD!");
                        break;
                    default:
                        Debug.Log("Warning: PowerupID was received as default (Powerup.cs)");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }

}
