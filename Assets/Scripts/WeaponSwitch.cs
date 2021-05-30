using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

	[SerializeField] GunController[] guns;

    int equippedGun = 0;

	void LateUpdate()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (Input.GetKeyDown("" + (i + 1)) && !guns[equippedGun].CheckReloadStatus() && i != equippedGun)
            {
                guns[equippedGun].gameObject.SetActive(false);
                guns[i].gameObject.SetActive(true);
                equippedGun = i;
            }
            else if (i != equippedGun)
            {
                guns[i].gameObject.SetActive(false);
            }
        }
    }
}
