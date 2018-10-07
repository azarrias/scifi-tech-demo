using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinPickUp;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player)
                {
                    player.hasCoin = true;
                    AudioSource.PlayClipAtPoint(coinPickUp, transform.position);
                    Destroy(gameObject);
                }
            }
        }
    }
}
