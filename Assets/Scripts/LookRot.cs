using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRot : MonoBehaviour
{

    public Transform target;


    void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        this.transform.rotation = rotation;
    }
}
