using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    // Variables
    [SerializeField]
    private float _speed = 20.0f;

    // Update is called once per frame
    void Update()
    {
        // if laser is spawned, it moves upwards
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if(transform.position.y >= 20.0f)
        {
            
            if(transform.parent != null)
            {
                //destroys TripleShot
                Destroy(transform.parent.gameObject);
            } 
            Destroy(this.gameObject);
        }
      
    }
}
