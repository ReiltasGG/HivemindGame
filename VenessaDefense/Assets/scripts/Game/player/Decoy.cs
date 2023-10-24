using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float _speed = 1f;
    private Camera cam;
    public float timeAlive = 5f;
     public GameObject[] potentialTargets;
    
   // private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
      potentialTargets = GameObject.FindGameObjectsWithTag("Enemy");
       
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

       private void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive-=Time.deltaTime;
        if(timeAlive <= 0)
        {
          //  Debug.Log(potentialTargets.Length);
           
      
            Destroy(gameObject);
        }
        rb.velocity = _speed * transform.up;
        DestroyWhenOffScreen();
    
    }
      private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = cam.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0 ||
           screenPosition.x > cam.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > cam.pixelHeight)
        {
            Destroy(gameObject);
        }
    }
}
