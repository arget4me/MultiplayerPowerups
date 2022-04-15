using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public Transform target;

    public float minDistance = 30.0f;
    public float cameraHeight = 5.0f;
    public float CameraAcc = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!target)
            return;

        transform.position = Vector3.Lerp(transform.position, target.transform.position - minDistance * target.transform.forward + Vector3.up * cameraHeight, Time.deltaTime * CameraAcc);
        transform.LookAt(target.transform, Vector3.up);
    }
}
