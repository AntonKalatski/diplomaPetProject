using System;
using UnityEngine;

namespace CommonBehaviours
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform camTransform;
        private void Awake()
        {
            camTransform = Camera.main.transform;
        }

        private void Update()
        {
            Quaternion rotation = camTransform.rotation;
            transform.LookAt(transform.position+rotation*Vector3.back,rotation * Vector3.up);
        }
    }
}
