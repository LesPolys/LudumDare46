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
                Debug.Log("Pickup");

                if (Input.GetMouseButtonDown(0))
                {

                    if (!hit.transform.gameObject.GetComponent<Connectable>().isConnected)
                    {
                        isGrabbing = true;
                        grabbedObject = hit.collider.gameObject;
                        hit.transform.SetParent(holdPoint, true);
                        hit.rigidbody.useGravity = false;
                        hit.rigidbody.isKinematic = true;
                        hit.transform.position = holdPoint.position;
                        hit.collider.enabled = false;
                    }
                    else
                    {
                        isGrabbing = true;
                        grabbedObject = hit.collider.gameObject.transform.root.gameObject;
                        grabbedObject.transform.SetParent(holdPoint, true);
                        grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.position = holdPoint.position;
                        grabbedObject.GetComponent<Collider>().enabled = false;
                    }

       

                    // hit.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    //Debug.Log("GRAB");
                }
                HandleHighlighting(hit.transform.gameObject);
            }
            else
            {
                UnHighlight();
            }

            Debug.DrawLine(cameraTransform.position + cameraTransform.forward * offsetDistance, hit.point, Color.red);

        }
        else
        {
            Debug.DrawRay(cameraTransform.position + cameraTransform.forward * offsetDistance, cameraTransform.forward * maxGrabDistance, Color.green);

            UnHighlight();
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(cameraTransform.position + cameraTransform.forward * offsetDistance, cameraTransform.forward, out hit, maxGrabDistance))
            {
                if (hit.collider.tag == "Pickup" && grabbedObject != null && !hit.transform.gameObject.GetComponent<Connectable>().isConnected  && !grabbedObject.GetComponent<Connectable>().isConnected)
                {
                    isGrabbing = false;
                    Transform connectionPoint = hit.transform.gameObject.GetComponent<Connectable>().connectionPoint.transform;
                    grabbedObject.layer = 10;

                    grabbedObject.transform.SetParent(connectionPoint);
                    grabbedObject.GetComponent<Collider>().enabled = true;

                    grabbedObject.transform.position = connectionPoint.position;
                    //grabbedObject.transform.rotation = connectionPoint.rotation;

                    hit.transform.gameObject.GetComponent<Connectable>().isConnected = true;
                    grabbedObject.GetComponent<Connectable>().isConnected = true;

                    grabbedObject = null;
                }
                Debug.DrawLine(cameraTransform.position + cameraTransform.forward * offsetDistance, hit.point, Color.red);

            }

            // Debug.Log("Release");
            if (grabbedObject != null && isGrabbing)
            {
                    isGrabbing = true;
                    grabbedObject.GetComponent<Rigidbody>().useGravity = true;
                    grabbedObject.GetComponent<Collider>().enabled = true;
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


    }

    void HandleHighlighting(GameObject newHighlightObject)
    {
        if (currentLookAtObject != newHighlightObject)
        {
            UnHighlight();
            currentLookAtObject = newHighlightObject;
        }

        if (currentLookAtObject != null && currentLookAtObject.GetComponent<Highlighting>())
        {
            currentLookAtObject.GetComponent<Highlighting>().Highlight();

        }

    }

    void UnHighlight()
    {
        if (currentLookAtObject)
        {
            currentLookAtObject.GetComponent<Highlighting>().UnHighlight();
        }
    }
}
