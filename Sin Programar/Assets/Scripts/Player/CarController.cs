using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    #region Auto Properties
    [SerializeField, HideInInspector] private InputPlayer input = null;
    [SerializeField, HideInInspector] private Rigidbody rb = null;
    #endregion
    
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float gravityForce;
    [SerializeField] private GameObject[] wheels;

    #region Auto Properties
#if UNITY_EDITOR
    private void OnEnable()
    {
        if(null == input)
            input = GetComponent<InputPlayer>();
        if(null == rb)
            rb = GetComponent<Rigidbody>();
    }
#endif
    #endregion

    void Update()
    {
        //Avoid flying car
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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
