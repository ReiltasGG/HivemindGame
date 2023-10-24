using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 healthbarOffset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            transform.rotation = cam.transform.rotation;
        }
        if (healthbarOffset != null && target != null)
        {
            transform.position = target.position + healthbarOffset;
        }
    }
}
