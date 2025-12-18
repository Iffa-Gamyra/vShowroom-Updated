using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    
    float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Move Forward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        //Move backwards
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        { 
            transform.Translate(-1 * Vector3.forward * Time.deltaTime * speed);
        }
        // Rotate Left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { 
            transform.Rotate(0, -0.5f, 0);
        }
        // Rotate right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0.5f, 0);
        }
    }
}