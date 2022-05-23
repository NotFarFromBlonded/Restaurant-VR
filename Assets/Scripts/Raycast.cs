using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public bool isWaiter = false;
    public bool isCashier = false;
    public bool isCustomer = false;
    
    public LayerMask layerMask;
    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 2.5f, layerMask))
        {
            if(hitinfo.collider.name == "Waiter")
            {

                isWaiter = true;
            }
            if (hitinfo.collider.name == "Cashier")
            {
                isCashier = true;
            }
            if (hitinfo.collider.tag.ToString() == "Customer")
            {
                isCustomer = true;
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);
        }
        else
        {
            isWaiter = false;
            isCashier = false;
            isCustomer = false;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2.5f, Color.green);
        }
    }

}
