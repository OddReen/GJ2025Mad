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

    SphereCollider thisCollider;
    RoomsManager manager;
    Animator elevatorAnim;
    public GameObject arrow;


    private void Start()
    {
        elevatorAnim = GetComponent<Animator>();
        manager = FindObjectOfType<RoomsManager>();
        thisCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            FMODMusicsController.instance.PauseMusic();
            FMODMusicsController.instance.PlayElevatorMusic();
            Debug.LogWarning("Player entered the elevator");
            StartCoroutine(ReplacePlayerPosition(other));
        }
    }

    private IEnumerator ReplacePlayerPosition(Collider player)
    {
        elevatorAnim.SetTrigger("CloseDoor");
        yield return new WaitForSeconds(2f);

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        this.transform.parent = null;
        manager.checkCurrentDecision(thisElevatorType, this);
        transform.position = manager.initialElevatorPosition;
        player.transform.position = transform.position;

        Destroy(manager.currentElevator);

        manager.currentElevator = this.gameObject;

        //Vector3 newPlayerPosition = transform.position;
        //newPlayerPosition.y -= 0.2f;

        player.transform.position = new Vector3(18.86755f, 1f, 13.23491f);

        yield return new WaitForSeconds(3f);
        elevatorAnim.SetTrigger("OpenDoor");

        if (controller != null)
        {
            controller.enabled = true;
            thisCollider.enabled = false;
        }
        //controller.enabled = true;
        arrow.SetActive(false);

        PlayerBehaviour playerScriptRef = FindObjectOfType<PlayerBehaviour>();

        playerScriptRef.gameState = PlayerBehaviour.GameState.WalkingUpToElevators;
        playerScriptRef.elevador = PlayerBehaviour.Elevador.Neutro;
        playerScriptRef.blockMovement = true;

        FMODMusicsController.instance.ResumeMusic();
    }
}
