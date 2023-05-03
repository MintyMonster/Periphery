using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Transform mainCam;

    public Transform worldSpaceCanvas;
    public Transform target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //mainCam = Camera.main.transform;
        //transform.SetParent(worldSpaceCanvas);
    }

    // Update is called once per frame

    // Made by James Pennell
    // Enables flaoting text.
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position); // look at camera

        transform.position = target.position + offset;
    }
}
