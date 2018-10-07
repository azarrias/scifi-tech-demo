using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkmanShop : MonoBehaviour
{
    private AudioSource audioSource;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player)
                {
                    if (player.hasCoin)
                    {
                        player.hasCoin = false;
                        player.EnableWeapon();
                        audioSource.Play();

                        if (uiManager)
                        {
                            uiManager.CoinSpent();
                        }
                    }
                    else
                    {
                        Debug.Log("No coin, no weapon!");
                    }
                }
            }
        }
    }
}
