using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 healthbarOffset;


    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        transform.rotation = cam.transform.rotation;
        transform.position = target.position + healthbarOffset;
    }
}
