using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAbilities : MonoBehaviour
{
    public float abilityTime;
    private float timeToDeactivate;
    private float recoveryTime;
    public float coolDownTime;
    private bool onCoolDown;
    private bool canDeactivate;
    private bool intangibleOn;
    
    public LayerMask obstaclesMask;
    public float sphereRadius;

    private Collider[] obstaclesCollider;
    private Collider[] obstaclesToReactivate;

    private MeshRenderer carMesh;
    public Material[] carMaterials;

    // Start is called before the first frame update
    void Start()
    {
        carMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        IntangibleAbility();
    }

    private void IntangibleAbility()
    {
        //Activate the ability, start the ability countdown time, and restart cooldown time. 
        if (Input.GetKeyDown(KeyCode.LeftShift) && !onCoolDown)
        {
            timeToDeactivate = Time.time + abilityTime;
            recoveryTime = Time.time + coolDownTime;
            onCoolDown = true;
            intangibleOn = true;
            carMesh.material = carMaterials[1];
        }

        //When the ability is activated, check all the obstacles colliders around a sphere and set those colliders triggers on. 
        //Keep the obstacles colliders in obstaclesToReactivate.
        if (intangibleOn)
        {
            obstaclesCollider = Physics.OverlapSphere(transform.position + Vector3.up, sphereRadius, obstaclesMask);
            foreach (Collider col in obstaclesCollider)
            {
                col.isTrigger = true;
                obstaclesToReactivate = new Collider[obstaclesCollider.Length];
                for (int i = 0; i < obstaclesCollider.Length; i++)
                {
                    obstaclesToReactivate[i] = col;
                }
            }
        }

        //This verify if we are trespassing an object to not deactivate the triggers and avoid the car getting stuck in an object when getting tangible again
        if (Physics.CheckSphere(transform.position + Vector3.up, sphereRadius, obstaclesMask))

        {
            canDeactivate = false;
        }
        else
        {
            canDeactivate = true;
        }

        //Set triggers off after a time (intangibleAbilityTime) using the obstacles that we save in obstaclesToReactivate array, only if we're not trespassing an object.
        if (timeToDeactivate < Time.time && canDeactivate && intangibleOn)
        {
            carMesh.material = carMaterials[0];
            foreach (Collider col in obstaclesToReactivate)
            {
                col.isTrigger = false;
            }
            obstaclesToReactivate = new Collider[0];
            intangibleOn = false;
        }

        //Finished the cooldown, allows us to activate the ability again.
        if (recoveryTime < Time.time)
        {
            onCoolDown = false;
        }
    }


    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + origin, sphereRadius);
    }
    */
    
}
