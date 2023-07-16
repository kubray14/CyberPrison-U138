using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    private GameObject grabbedObject;
    private bool isGrabbing = false;
    private Vector3 offset;

    void Update()
    {
        if (isGrabbing) 
        {
            // Eldeki objenin konumunu güncelle
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0.75f; // Kameranın uzaklığına bağlı olarak ayarlayın
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            grabbedObject.transform.position = targetPosition;

            if (!Input.GetMouseButton(0))
            {
                // Fare butonu bırakıldığında objeyi bırak
                ReleaseObject();
            }
        }
        else
        {
            // Farenin sol düğmesine basıldığında objeyi tut
            if (Input.GetMouseButtonDown(0))
            {
                GrabObject();
            }
        }
    }

    void GrabObject()
    {
        // Fare raycast ile objeyi tespit et
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                // Tutulabilir objeyi tut
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                offset = grabbedObject.transform.position - transform.position;
                isGrabbing = true;
            }
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            // Tutulan objeyi bırak
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject = null;
            isGrabbing = false;
        }
    }
}
