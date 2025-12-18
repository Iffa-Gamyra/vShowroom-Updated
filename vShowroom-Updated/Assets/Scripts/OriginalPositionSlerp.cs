using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalPositionSlerp : MonoBehaviour
{
    public float speed = -1f;
    public GameObject ObjectToRotate;
    public Quaternion originalRot;
    public bool original;
    public void Start()
    {
        originalRot = ObjectToRotate.transform.rotation;

    }
    public void originalRotation()
    {
        original = true;
        //  ObjectToRotate.transform.rotation = originalRot;

    }
    public void Update()
    {
        if (original == true)
        {
            ObjectToRotate.transform.rotation = Quaternion.Slerp(ObjectToRotate.transform.rotation, originalRot, Time.deltaTime * 0.5f);
            /*if (ObjectToRotate.transform.rotation == originalRot)
            {
                original = false;
            }*/
            if ((Quaternion.Dot(ObjectToRotate.transform.rotation, originalRot) > 0.9999f))
            {
                original = false;

            }

        }
    }
}
