using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC_ObjectTrade : MonoBehaviour
{
    private PickUpController pickupController;
    public string requiredObjectID;

    public UnityEvent eventToPlay;


    private void Start()
    {
        pickupController = FindObjectOfType<PickUpController>();

    }

    private void OnTriggerStay(Collider other)
    {
        if (requiredObjectID == pickupController.currentObjectHolding)
        {
            eventToPlay.Invoke();

        }
    }
}
