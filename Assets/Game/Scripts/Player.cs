using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private CharacterController controller;
    [SerializeField]
    private float speed = 3.5f;
    private float gravity = 9.81f;

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
        // If mouse left click cast ray from the main camera through the center of the screen
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("RayCast Hit " + hitInfo.transform.name + "!");
            }
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
