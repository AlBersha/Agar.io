using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public string Tag;
    public float Increase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag))
        {
            transform.localScale += new Vector3(Increase, Increase, Increase);
            Destroy(other.gameObject);
        }
    }
}
