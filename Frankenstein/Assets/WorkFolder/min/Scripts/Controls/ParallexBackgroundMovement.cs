using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackgroundMovement : MonoBehaviour
{
    [SerializeField]
    private Transform mainCameraPosition;

    [SerializeField]
    private float backgroundMovementSpeed;
    private float directionX;

    [SerializeField]
    private float offsetByX = 13f;
   

    // Update is called once per frame
    void Update()
    {
        directionX = Input.GetAxis("Horizontal") * backgroundMovementSpeed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + directionX,
            transform.position.y);

        if (transform.position.x - mainCameraPosition.position.x < -offsetByX)
            transform.position = new Vector2(mainCameraPosition.position.x + offsetByX,
                transform.position.y);

        else if (transform.position.x - mainCameraPosition.position.x > offsetByX)
            transform.position = new Vector2(mainCameraPosition.position.x - offsetByX,
                transform.position.y);
    }
}
