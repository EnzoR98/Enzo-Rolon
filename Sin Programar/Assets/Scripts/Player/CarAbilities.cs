using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAbilities : MonoBehaviour
{
    private MeshCollider carCollider;
    public float abilityTime;
    private float recoveryTime;
    public float coolDownTime;

    public LayerMask obstaclesMask;
    public float sphereRadius;
    [HideInInspector] public Vector3 origin;

    private MeshRenderer carMesh;
    public Material[] carMaterials;                       

    #region Luciano
    public bool IsActive { get; set; }

    private Coroutine passThroughRoutine = null;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        carCollider = GetComponent<MeshCollider>();
        carMesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (null == passThroughRoutine)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                passThroughRoutine = StartCoroutine(PassTrhoughRoutine());
            }
        }
    }

    private IEnumerator PassTrhoughRoutine()
    {
        // Activo el efecto
        SetPassThroughState(true);

        // Espero la duracion
        yield return new WaitForSeconds(abilityTime);

        // Cuando finaliza la duracion, espero salir de posible penetracion
        yield return StartCoroutine(CheckSphereRoutine());

        // Termino duracion y no hay penetracion, desactivo el efecto
        SetPassThroughState(false);

        // Una vez que el efecto realmente esta desactivado, empiezo el CD
        yield return new WaitForSeconds(recoveryTime);

        passThroughRoutine = null;
    }

    private void SetPassThroughState(bool state)
    {
        IsActive = state;
        carCollider.isTrigger = state;
        carMesh.material = carMaterials[state ? 1 : 0];
    }

    IEnumerator CheckSphereRoutine()
    {
        do
        {
            yield return new WaitForFixedUpdate();
        } while (Physics.CheckSphere(transform.position + origin, sphereRadius, obstaclesMask));
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + origin, sphereRadius);
    }
    */

}