using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Variables
    [SerializeField]
    private float _speed = 3.0f;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
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
            player._isTripleShotActive = true;
            Destroy(gameObject);
        }

    }
}
