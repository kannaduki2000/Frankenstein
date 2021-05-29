using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -53f, 128f),
            Mathf.Clamp(transform.position.y, -100f, 21f), transform.position.z); 
    }
}
