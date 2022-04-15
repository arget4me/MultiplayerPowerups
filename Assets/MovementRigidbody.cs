using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class MovementRigidbody : NetworkBehaviour 
{

    private Rigidbody rb;
    private FollowTarget cameraFollow;
    float forward = 0;
    float strafe = 0;
    float up = 0;

    float cameraRot = 0;

    const float ZERO_THRESHOLD = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer)
            return;
        rb = GetComponent<Rigidbody>();
        cameraFollow = Camera.main.gameObject.GetComponent<FollowTarget>();
        cameraFollow.target = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer)
            return;

        forward = Input.GetAxis("Vertical");
        strafe = Input.GetAxis("Horizontal");
        up = Input.GetAxis("Jump");
        cameraRot = Input.GetAxis("Mouse X");
        cameraFollow.cameraHeight = 2.0f + ((Input.GetAxis("Mouse Y") + 1.0f) / 2.0f) * 10.0f;
    }

    void FixedUpdate()
    {
        if(!isLocalPlayer)
            return;

        transform.Rotate(0, cameraRot * 10.0f, 0);   
        if(Mathf.Abs(forward) > ZERO_THRESHOLD)
        {
            rb.AddForce(forward * transform.forward, ForceMode.Impulse);
        }
        if(Mathf.Abs(strafe) > ZERO_THRESHOLD)
        {
            rb.AddForce(strafe * transform.right, ForceMode.Impulse);
        }
        if(Mathf.Abs(up) > ZERO_THRESHOLD)
        {
            rb.AddForce(up * transform.up, ForceMode.Impulse);
        }
    }
}
