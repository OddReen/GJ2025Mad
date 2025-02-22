using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float speed;

    Elevador elevador = Elevador.Neutro;
    GameState gameState = GameState.WalkingUpToElevators;

    [SerializeField] Transform endOfCorridor;

    [SerializeField] Transform neutro;
    [SerializeField] Transform maravilha;
    [SerializeField] Transform grotesco;

    enum GameState
    {
        WalkingUpToElevators,
        GettingInElevator,
        StayingInElevator
    }
    enum Elevador
    {
        Neutro,
        Maravilha,
        Grotesco
    }

    private void Update()
    {
        if (gameState == GameState.WalkingUpToElevators)
        {
            if (HasReachedDestination(endOfCorridor.position))
            {
                gameState = GameState.GettingInElevator;
            }
            MoveTowards(endOfCorridor.position);
            MoveSideWays();
        }
        else if (gameState == GameState.GettingInElevator)
        {
            if (elevador == Elevador.Neutro)
            {
                MoveTowards(neutro.position);
                if (HasReachedDestination(neutro.position))
                {
                    gameState = GameState.StayingInElevator;
                }
            }
            if (elevador == Elevador.Maravilha)
            {
                MoveTowards(maravilha.position);
                if (HasReachedDestination(maravilha.position))
                {
                    gameState = GameState.StayingInElevator;
                }
            }
            if (elevador == Elevador.Grotesco)
            {
                MoveTowards(grotesco.position);
                if (HasReachedDestination(grotesco.position))
                {
                    gameState = GameState.StayingInElevator;
                }
            }
        }
        else if (gameState == GameState.StayingInElevator)
        {
            // If right, go up a level
            // If wrong, restart
        }
    }
    void MoveTowards(Vector3 destination)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);

    }
    void MoveSideWays()
    {
        if (elevador != Elevador.Maravilha && Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            elevador = Elevador.Maravilha;
        }
        else if (elevador != Elevador.Neutro && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            elevador = Elevador.Neutro;
        }
        else if (elevador != Elevador.Grotesco && Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            elevador = Elevador.Grotesco;
        }
    }
    bool HasReachedDestination(Vector3 destination)
    {
        if (Vector3.Distance(destination, transform.position) < .1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
