using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableObstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Vehicle"))
        {
            //Change car speed to 0.
            CarController vehicle = col.gameObject.GetComponent<CarController>();
            vehicle.rb.velocity = Vector3.zero;
        }
    }
    
}
