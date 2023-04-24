using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{

    private LineRenderer lineRenderer;

    private GameObject light;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) 
        {

            if (hit.collider) {
                lineRenderer.SetPosition(1, hit.point);
            }
               
            
        }
        else {
            lineRenderer.SetPosition(1, transform.forward*5000);
        }
           
    }
}

