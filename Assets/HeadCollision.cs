using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollision : MonoBehaviour
{
    public TrickManager trickManager;

    public void OnTriggerEnter(Collider other)
    {
        trickManager.Wipeout();
    }
}
