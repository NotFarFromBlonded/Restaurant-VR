using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{ 
    Rigidbody rb;
    public float power = 5;
    public float torque = 0.5f;
    public float maxSpeed = 5;

    public Vector2 mVector;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 mInput)
    {
        this.mVector = mInput;
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(mVector.y * transform.forward * power);
        }
        rb.AddTorque(mVector.x * Vector3.up * torque * mVector.y);
    }
}
