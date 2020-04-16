using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableObstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Vehicle"))
        {
           col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    
}
