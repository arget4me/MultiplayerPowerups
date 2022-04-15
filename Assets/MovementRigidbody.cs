using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class MovementRigidbody : NetworkBehaviour 
{

    private Rigidbody rb;
    private FollowTarget cameraFollow;
    private Animator anim;
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
        anim = GetComponentInChildren<Animator>();
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
        cameraFollow.cameraHeight += Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        if(!isLocalPlayer)
            return;

        transform.Rotate(0, cameraRot * 20.0f, 0);   
        if(Mathf.Abs(forward) > ZERO_THRESHOLD && Mathf.Abs(Vector3.Dot(rb.velocity, transform.forward)) < 50.0f )
        {
            rb.AddForce(forward * transform.forward, ForceMode.Impulse);
        }
        if(Mathf.Abs(strafe) > ZERO_THRESHOLD && Mathf.Abs(Vector3.Dot(rb.velocity, transform.right)) < 50.0f)
        {
            rb.AddForce(strafe * transform.right, ForceMode.Impulse);
        }
        if(Mathf.Abs(up) > ZERO_THRESHOLD && Vector3.Dot(rb.velocity, Vector3.up) < 10.0f)
        {
            rb.AddForce(up * transform.up, ForceMode.Impulse);
        }
        rb.velocity.Scale(new Vector3(0.97f, 1.0f, 0.97f));

        if((new Vector3(rb.velocity.x, 0, rb.velocity.z)).magnitude > 10.0f)
        {
            anim.speed = 1.0f;
        }
        else
        {
            anim.speed = 0.0f;
        }

    }
}
