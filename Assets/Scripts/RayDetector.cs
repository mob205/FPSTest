using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDetector : MonoBehaviour {

    public void DetectRayHit(float damage)
    {
        SendMessage("ProcessRayHit", damage);
    }

}
