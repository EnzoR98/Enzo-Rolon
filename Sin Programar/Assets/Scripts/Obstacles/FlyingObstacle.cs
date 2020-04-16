using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObstacle : MonoBehaviour
{
    public float force;
    Vector3 direction;
    Rigidbody rb;
    float[] axisRotationValues = new float [3];
    private bool flying;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ObstacleRandomRotation();
    }

    private void ObstacleRandomRotation()
    {
        if (flying)
        {
            axisRotationValues[0] += axisRotationValues[0];
            axisRotationValues[1] += axisRotationValues[1];
            axisRotationValues[2] += axisRotationValues[2];
            transform.rotation = Quaternion.Euler(axisRotationValues[0], axisRotationValues[1], axisRotationValues[2]);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Vehicle"))
        {
            direction = transform.position - col.transform.position + new Vector3(0, 0.5f, 0);

            //Add a Force relative to car impact direction and its speed.
            rb.AddRelativeForce(force * col.impulse, ForceMode.Impulse);

            //Assign 3 random values that will be used to rotate this asset.
            for(int i = 0; i<axisRotationValues.Length; i++)
            {
                axisRotationValues[i] = Random.Range(0, 30f);
            }
            
            flying = true;

            Destroy(this.gameObject, 1f); //ALERT Check how this works whit more than 2 seconds.
            
        }
    }
    
}
