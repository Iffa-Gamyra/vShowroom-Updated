using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    public int Speed;

    void Update()
    {
        //Touch touchZero = Input.GetTouch(0);

        if (Input.GetMouseButton(0))
        {
   
            transform.eulerAngles += Speed * new Vector3(0, -Input.GetAxis("Mouse X"), 0);
        }

        /*if(Input.touchCount == 1)
        {
            //transform.eulerAngles += Speed * new Vector3(0, -touchZero, 0);
        }*/
    }
}