using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetToFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -36f, 39f), // ÉÇÉbÉNî≈åFëq:Ç±Ç±ÇÁÇ÷ÇÒè≠ÇµèëÇ´ä∑Ç¶Ç‹ÇµÇΩ
            Mathf.Clamp(targetToFollow.position.y, 9.7f, 9.7f),
            transform.position.z);
    }
}
