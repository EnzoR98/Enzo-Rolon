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
    public GameObject[] wheels;
    // Start is called before the first frame update

    void Start()
    {
        input = GetComponent<InputPlayer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Avoid flying car
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0.03f, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        CarMovement();

        //Add a Gravity Force.
        rb.AddForce(Vector3.down * gravityForce);
    }

    void CarMovement()
    {
        //Accelerate, decelerate
        rb.velocity = transform.forward * input.yAxis * speed * Time.deltaTime;

        //Turn
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationAngle, transform.rotation.z);

        //Needs to be accelerating a little bit to rotate the car;
        if (input.yAxis < -0.2f || input.yAxis > 0.2f)
        {
            rotationAngle += (input.xAxis * rotationSpeed);
            if (rotationAngle > 360 || rotationAngle < -360)
            {
                rotationAngle = 0;
            }
        }
    }
}
