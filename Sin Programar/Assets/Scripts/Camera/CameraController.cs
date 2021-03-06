﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;
    public float yPos;
    public float zAxisDistance;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<CarController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, yPos, playerTransform.position.z - zAxisDistance);
    }
}
