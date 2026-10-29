using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed);
    }
}
