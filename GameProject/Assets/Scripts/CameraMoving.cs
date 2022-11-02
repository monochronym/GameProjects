using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundZ = 0.15f;
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
                delta.x = deltaX - boundX;
            else delta.x = deltaX + boundX;
        }
        float deltaZ = lookAt.position.z - transform.position.z;
        if (deltaZ > boundZ || deltaZ < -boundZ)
        {
            if (transform.position.z < lookAt.position.z)
                delta.z = deltaZ - boundZ;
            else delta.z = deltaZ + boundZ;
        }
        transform.position += new Vector3(delta.x, 0,delta.z+10);
    }
}
