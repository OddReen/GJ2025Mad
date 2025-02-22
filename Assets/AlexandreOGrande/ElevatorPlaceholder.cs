using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Elevatortype
{
    Good,
    bad,
    neutral
}

public class ElevatorPlaceholder : MonoBehaviour
{

    public Elevatortype thisElevatorType;

    RoomsManager manager;
    Animator elevatorAnim;

    private void Start()
    {
        elevatorAnim = GetComponent<Animator>();
        manager = FindObjectOfType<RoomsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
            elevatorAnim.SetTrigger("CloseDoor");
            Debug.LogWarning("Entered in collider");
            StartCoroutine(ReplacePlayerPosition());

        }
    }

    public IEnumerator ReplacePlayerPosition()
    {
        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(4.336629f, .63f, -2.818707f);
        yield return new WaitForSeconds(3f);
        elevatorAnim.SetTrigger("OpenDoor");

    }
}
