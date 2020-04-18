using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    Transform holdPoint;

    [SerializeField]
    float offsetDistance;

    [SerializeField]
    float maxGrabDistance;

    private GameObject grabbedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLICK");
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position + cameraTransform.forward * offsetDistance, cameraTransform.forward, out hit, maxGrabDistance))
            {
                if (hit.collider.tag == "Pickup")
                {
                    Debug.Log("GRAB");
                }
            }
        }
        Debug.DrawRay(cameraTransform.position + cameraTransform.forward * offsetDistance, cameraTransform.forward * maxGrabDistance, Color.green);
    }
}
