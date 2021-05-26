using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpReloadController : GunController {

    [SerializeField] float reloadDelay;
    public override IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadDelay);
        while(_ammoCount < magSize)
        {
            _ammoCount += 1;
            yield return new WaitForSeconds(reloadTime);
        }
        isReloading = false;
    }
}
