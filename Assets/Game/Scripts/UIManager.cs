using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text ammoText;

    [SerializeField]
    private GameObject coin;

	public void UpdateAmmo(int count)
    {
        ammoText.text = "Ammo: " + count;
    }

    public void CoinCollected()
    {
        coin.SetActive(true);
    }
}
