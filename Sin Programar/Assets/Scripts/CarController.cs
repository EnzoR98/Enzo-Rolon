using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private InputPlayer input;
    [HideInInspector]public Rigidbody rb;
    public float speed;
    public float rotationSpeed;
    public float rotationAngle;
    public float maxSpeed;
    public float gravityForce;
    // Start is called before the first frame update

    void Start()
    {
        input = GetComponent<InputPlayer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Needs to be accelerating a little bit to rotate the car;
        if(input.yAxis < -0.2f || input.yAxis > 0.2f)
        {
            rotationAngle += (input.xAxis * rotationSpeed);
            if(rotationAngle > 360 || rotationAngle < -360)
            {
                rotationAngle = 0;
            }
        }
        
        //Avoid flying car
        if (transform.position.y > 0.03f)
        {
            transform.position = new Vector3(transform.position.x, 0.03f, transform.position.z);
        }

        //Add a Gravity Force.
        rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
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
