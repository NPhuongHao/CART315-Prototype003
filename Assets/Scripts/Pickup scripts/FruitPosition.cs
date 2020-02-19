using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPosition : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.WALLG || other.tag == Tags.WALLO || other.tag == Tags.WALLB)
        {
            gameObject.SetActive(false);
            GameplayController.instance.StartSpawning();
        }
    }
}
