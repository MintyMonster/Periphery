using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    //Made by James Sherlock

    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform laserPostion;

    //Gets the linerenderer component and sets its position 
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, laserPostion.position);
    }

    //Fires out a Raycast and uses the point it hits in order to render out a line
    //This is done to the Prism's a laser effect that stops when it hits a wall
    void Update()
    {
       
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

