using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private CharacterController controller;
    [SerializeField]
    private float speed = 3.5f;
    private float gravity = 9.81f;
    [SerializeField]
    private GameObject muzzleFlash;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
 	}
	
	// Update is called once per frame
	void Update ()
    {
        // If holding mouse left click cast ray from the main camera through the center of the viewport
        if (Input.GetMouseButton(0))
        {
            muzzleFlash.SetActive(true);
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("RayCast Hit " + hitInfo.transform.name + "!");
            }
        }
        else
        {
            muzzleFlash.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        CalculateMovement();
	}

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        direction = transform.transform.TransformDirection(direction);

        Vector3 velocity = direction * speed;
        velocity.y -= gravity;

        controller.Move(velocity * Time.deltaTime);
    }
}
