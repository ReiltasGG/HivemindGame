using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Transform _transform;
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
      //  player.GetComponent<Animator>().enabled = false;
        

        _transform = this.transform;
    }

    private void LAMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        _transform.rotation = rotation;
    }
    // Update is called once per frame
    void Update()
    {
        LAMouse();
    }
}
