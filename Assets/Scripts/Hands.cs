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

    private bool isGrabbing = false;

    private GameObject currentLookAtObject;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CLICK");
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position + cameraTransform.forward * offsetDistance, cameraTransform.forward, out hit, maxGrabDistance))
        {
            if (hit.collider.tag == "Pickup")
            {
                if (Input.GetMouseButtonDown(0))
                {
                
                    isGrabbing = true;
                    grabbedObject = hit.collider.gameObject;
                    hit.transform.SetParent(holdPoint, true);
                    hit.rigidbody.useGravity = false;
                    hit.rigidbody.isKinematic = true;
                    hit.transform.position = holdPoint.position;
                    //hit.collider.enabled = false;

                    // hit.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    //Debug.Log("GRAB");
                }
                HandleHighlighting(hit.transform.gameObject);
            }
            else
            {
                UnHighlight();
            }
        }
        else
        {
            UnHighlight();
        }


        if (Input.GetMouseButtonUp(0))
        {
           // Debug.Log("Release");
            if (grabbedObject != null && isGrabbing)
            {
                    isGrabbing = true;
                    grabbedObject.GetComponent<Rigidbody>().useGravity = true;
                    //grabbedObject.GetComponent<Collider>().enabled = true;
                    grabbedObject.transform.SetParent(null);
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                    //grabbedObject.transform.localScale = new Vector3(1, 1, 1);
                    grabbedObject = null;
                   // Debug.Log("Release");
            }
        }

        if (grabbedObject)
        {
            HandleHighlighting(grabbedObject);
        }

        Debug.DrawRay(cameraTransform.position + cameraTransform.forward * offsetDistance, cameraTransform.forward * maxGrabDistance, Color.green);
    }

    void HandleHighlighting(GameObject newHighlightObject)
    {
        if (currentLookAtObject != newHighlightObject)
        {
            UnHighlight();
            currentLookAtObject = newHighlightObject;
        }
        currentLookAtObject.GetComponent<Highlighting>().Highlight();

    }

    void UnHighlight()
    {
        if (currentLookAtObject)
        {
            currentLookAtObject.GetComponent<Highlighting>().UnHighlight();
        }
    }
}
