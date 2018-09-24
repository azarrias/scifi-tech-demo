using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private CharacterController controller;
    [SerializeField]
    private float speed = 3.5f;
    private float gravity = 9.81f;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
 	}
	
	// Update is called once per frame
	void Update () {
        CalculateMovement();
	}

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * speed;
        velocity.y -= gravity;
        controller.Move(velocity * Time.deltaTime);
    }
}
