using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardsCamera : MonoBehaviour
{

    public Transform mainCamera;

    // Update is called once per frame
    void Update() {
        transform.LookAt(mainCamera, -Vector3.up);
        // then lock rotation to Y axis only...
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
