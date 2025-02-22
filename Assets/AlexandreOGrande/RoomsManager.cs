using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    public GameObject neutralRoomPrefab;
    public GameObject[] GoodRoomPrefab;
    public GameObject[] BadRoomPrefab;

    public Elevatortype currentLevelType = Elevatortype.neutral;
    public GameObject currentElevator;
    public GameObject currentRoom;

    [SerializeField] int currentScore;

    public Vector3 initialElevatorPosition;
    public Vector3 initialRoomLocation;

    private void Start()
    {
        initialElevatorPosition = currentElevator.transform.position;
        initialRoomLocation = currentRoom.transform.position;
    }
    public void checkCurrentDecision(Elevatortype elevatorEntered)
    {
        if (elevatorEntered == currentLevelType)
        {
            currentScore++;
        }
        else
        {
            currentScore = 0;
        }

        GenerateRandomRoom();
    }

    public void GenerateRandomRoom()
    {
        int randomRoomType = Random.Range(0, 3);

        Destroy(currentRoom);

        switch (randomRoomType)
        {
            case 0:
                currentRoom = Instantiate(neutralRoomPrefab, initialRoomLocation, Quaternion.identity);
                currentLevelType = Elevatortype.neutral;
                break;
            case 1:
                currentRoom = Instantiate(GoodRoomPrefab[Random.Range(0, GoodRoomPrefab.Length)], initialRoomLocation, Quaternion.identity);
                currentLevelType = Elevatortype.Good;
                break;
            case 2:
                currentRoom = Instantiate(BadRoomPrefab[Random.Range(0, BadRoomPrefab.Length)], initialRoomLocation, Quaternion.identity);
                currentLevelType = Elevatortype.bad;
                break;
        }

    }
}