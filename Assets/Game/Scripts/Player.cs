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
    [SerializeField]
    private GameObject hitMarkerPrefab;
    [SerializeField]
    private AudioSource weaponAudio;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private bool isReloading = false;

    private UIManager uiManager;

    public bool hasCoin = false;

    [SerializeField]
    private GameObject weapon;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentAmmo = maxAmmo;

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
 	}
	
	// Update is called once per frame
	void Update ()
    {
        // If holding mouse left click cast ray from the main camera through the center of the viewport
        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            Shoot();
        }
        else
        {
            muzzleFlash.SetActive(false);
            weaponAudio.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }

        CalculateMovement();
	}

    void Shoot()
    {
        muzzleFlash.SetActive(true);
        currentAmmo--;
        uiManager.UpdateAmmo(currentAmmo);

        if (!weaponAudio.isPlaying)
        {
            weaponAudio.Play();
        }

        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("RayCast Hit " + hitInfo.transform.name + "!");
            GameObject hitMarker = Instantiate(hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            Destroy(hitMarker, 0.5f);

            // If we hit any destructable
            Destructable destructable = hitInfo.transform.GetComponent<Destructable>();
            if (destructable)
            {
                destructable.DestroyObject();
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        uiManager.UpdateAmmo(currentAmmo);
        isReloading = false;
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

    public void EnableWeapon()
    {
        weapon.SetActive(true);
    }
}
