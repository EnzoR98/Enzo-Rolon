using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObstacle : MonoBehaviour
{
    public float fuerza;
    Vector3 direccion;
    Rigidbody rb;
    float xRotation;
    float yRotation;
    float zRotation;
    private bool volando;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (volando)
        {
            xRotation += xRotation;
            yRotation += yRotation;
            zRotation += zRotation;
            transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Vehicle"))
        {
            CarController vehicle = col.gameObject.GetComponent<CarController>();
            direccion = transform.position - col.transform.position;
            rb.AddRelativeForce(direccion.normalized * fuerza * vehicle.rb.velocity.z , ForceMode.Impulse);
            xRotation = Random.Range(0, 30);
            yRotation = Random.Range(0, 30);
            zRotation = Random.Range(0, 30);
            volando = true;
            Destroy(this.gameObject, 1f);
            
        }
    }
}
