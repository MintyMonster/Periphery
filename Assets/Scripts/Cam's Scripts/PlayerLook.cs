using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [Header("Look parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    private float rotationX = 0f;
    private Camera playerCamera;

    private float timer = 0.0f;
    private float period = 0.2f;

    // Start is called before the first frame update
    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<FirstPersonController>().CanMove)
        {
            HandleMouseLook();
            HandleEnemyLook();
        }
    }

    /// <summary>
    /// Mouse rotation handler
    /// </summary>
    private void HandleMouseLook()
    {
        if (!journal.isPaused)
        {
            // Rotate at speed
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
            // Clamp limits (up/down)
            rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
            // Rotate camera
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            // Rotate transform
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
        }

    }

    /// <summary>
    /// "Glance" / "Stare" handler for enemies
    /// </summary>
    private void HandleEnemyLook()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 100f, Color.red);
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, 100f))
            if (hit.transform.tag == "Enemy")
            {
                SeenMeter meter = hit.transform.GetComponent<SeenMeter>();
                EnemyAI ai = hit.transform.GetComponent<EnemyAI>();

                timer += Time.deltaTime;

                if(timer > period)
                {
                    timer -= period;

                    meter.Seen = true;
                    meter.AddSeen();
                    meter.player = this.transform;
                }
            }
    }
}
