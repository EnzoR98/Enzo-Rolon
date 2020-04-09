using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableObstacle : MonoBehaviour
{
    public float pushForce;
    public float jumpForce;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Vehicle"))
        {
            CarController vehicle = col.gameObject.GetComponent<CarController>();
            vehicle.rb.AddForce(new Vector3(0, 1 * jumpForce, -1 * pushForce), ForceMode.Impulse);
        }
    }
}
