using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAbilities : MonoBehaviour
{
    private MeshCollider carCollider;
    public float abilityTime;
    private float timeToDeactivate;
    private float recoveryTime;
    public float coolDownTime;
    private bool onCoolDown;
    private bool canDeactivate;
    
    public LayerMask obstaclesMask;
    public float sphereRadius;
    [HideInInspector]public Vector3 origin;

    private MeshRenderer carMesh;
    public Material[] carMaterials;

    // Start is called before the first frame update
    void Start()
    {
        carCollider = GetComponent<MeshCollider>();
        carMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        IntangibleAbility();
    }

    private void IntangibleAbility()
    {
        //Turn car collider a trigger, start the ability countdown time, and restart cooldown time.
        if (Input.GetKeyDown(KeyCode.LeftShift) && !onCoolDown)
        {
            carCollider.isTrigger = true;

            timeToDeactivate = Time.time + abilityTime;
            recoveryTime = Time.time + coolDownTime;
            onCoolDown = true;

            carMesh.material = carMaterials[1];
        }

        //This verify if we are trespassing an object to not deactivate the trigger and avoid the car getting stuck in an object when getting tangible again
        if (Physics.CheckSphere(transform.position + origin, sphereRadius, obstaclesMask))
        {
            canDeactivate = false;
        }
        else
        {
            canDeactivate = true;
        }

        //Deactivate the trigger after a time (intangibleAbilityTime).
        if (timeToDeactivate < Time.time && canDeactivate)
        {
            carCollider.isTrigger = false;
            carMesh.material = carMaterials[0];
        }

        //Finish the cooldown, allows to activate the ability again.
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
