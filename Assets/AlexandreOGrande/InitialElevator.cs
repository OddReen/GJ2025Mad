using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialElevator : MonoBehaviour
{
    Animator anim;
    public GameObject uiToDESTROY;
    public GameObject DirUI;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("OpenDoor");
            uiToDESTROY.SetActive(false);
            DirUI.SetActive(true);
            FindObjectOfType<PlayerBehaviour>().StartGame();
            FindObjectOfType<CameraRotation>().enabled = true;
            Destroy(this);
        }
    }
}
