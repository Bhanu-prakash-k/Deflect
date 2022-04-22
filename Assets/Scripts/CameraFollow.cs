using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    public float smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
