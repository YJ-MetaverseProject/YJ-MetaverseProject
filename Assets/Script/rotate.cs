using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float speed = 3f;

    void Update()
    {
        transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
    }
}
