using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private InputPlayer input;
    private Rigidbody rb;
    public float speed;
    public float rotationSpeed;
    public float rotationAngle;
    public float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputPlayer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(input.yAxis < -0.2f || input.yAxis > 0.2f)
        {
            rotationAngle += (input.xAxis * rotationSpeed);
            if(rotationAngle > 360 || rotationAngle < -360)
            {
                rotationAngle = 0;
            }
        }
        //Debug.Log(rigidbody.velocity.z);
    }

    private void FixedUpdate()
    {
        CarMovement();
    }



    void CarMovement()
    {
        //Accelerate, decelerate
        rb.velocity = transform.forward * input.yAxis * speed * Time.deltaTime;

        //Turn
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationAngle, transform.rotation.z);
    }
}
