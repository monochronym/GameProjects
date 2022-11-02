using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving: MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 move;
    public Vector3 rot;
    public Rigidbody body;
    public float speedX;
    public float speedY;
    public float speedZ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move.x = Input.GetKey(KeyCode.A) ? -speedX : 0;
        move.x = Input.GetKey(KeyCode.D) ?  speedX : move.x;
        move.y = Input.GetKey(KeyCode.S) ? (body.position.y >= -518 ? -speedY : 0) : 0;
        move.y = Input.GetKey(KeyCode.W) ? (body.position.y <= -187 ? speedY : move.y) : move.y;
        if (transform.position.y <= -441) transform.position = new Vector3(transform.position.x, transform.position.y, -253);
        else transform.position = new Vector3(transform.position.x, transform.position.y, -251);
        body.position += move;
    }
}
