using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private GameObject specificCamera;

    void Start()
    {
        specificCamera = GameObject.Find("Main Camera");
        if (specificCamera == null)
        {
            Debug.LogError("Camera not found");
        }
    }

    void Update()
    {
        if (specificCamera != null)
        {
            transform.LookAt(specificCamera.transform);
        }
    }
}