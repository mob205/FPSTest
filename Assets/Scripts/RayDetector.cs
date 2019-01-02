using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDetector : MonoBehaviour {

    public void DetectRayHit(int damage)
    {
        SendMessage("ProcessRayHit", damage);
    }

}
