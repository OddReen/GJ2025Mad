using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float speed;

    public Elevador elevador = Elevador.Neutro;
    public GameState gameState = GameState.StayingInElevator;

    [SerializeField] Transform endOfCorridor;

    [SerializeField] Transform neutro;
    [SerializeField] Transform maravilha;
    [SerializeField] Transform grotesco;

    [SerializeField] Animator neutroAnim;
    [SerializeField] Animator maravilhaAnim;
    [SerializeField] Animator grotescoAnim;

    [SerializeField] GameObject DirUI;
    [SerializeField] GameObject FloorNum;

    Animator cameraAnim;

    bool started;
    public bool blockMovement = true;
    public enum GameState
    {
        WalkingUpToElevators,
        GettingInElevator,
        StayingInElevator
    }
    public enum Elevador
    {
        Neutro,
        Maravilha,
        Grotesco
    }

    public void StartGame()
    {
        if (!started)
        {
            Invoke(nameof(StartGameDelay), 1);
        }
    }
    void StartGameDelay()
    {
        started = true;
        FloorNum.SetActive(true);
        gameState = GameState.WalkingUpToElevators;
    }

    private void Start()
    {
        cameraAnim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (started)
        {
            if (gameState == GameState.WalkingUpToElevators)
            {
                DirUI.SetActive(true);
                UpdateCameraAnimator(true);
                if (HasReachedDestination(endOfCorridor.position))
                {
                    gameState = GameState.GettingInElevator;
                }
                MoveTowards(endOfCorridor.position);
                if (blockMovement)
                {
                    MoveSideWays();
                }
            }
            else if (gameState == GameState.GettingInElevator)
            {

                if (elevador == Elevador.Neutro)
                {
                    MoveTowards(neutro.position);
                    if (HasReachedDestination(neutro.position))
                    {
                        gameState = GameState.StayingInElevator;
                        UpdateCameraAnimator(false);
                    }
                }
                if (elevador == Elevador.Maravilha)
                {
                    MoveTowards(maravilha.position);
                    if (HasReachedDestination(maravilha.position))
                    {
                        gameState = GameState.StayingInElevator;
                        UpdateCameraAnimator(false);
                    }
                }
                if (elevador == Elevador.Grotesco)
                {
                    MoveTowards(grotesco.position);
                    if (HasReachedDestination(grotesco.position))
                    {
                        gameState = GameState.StayingInElevator;
                        UpdateCameraAnimator(false);
                    }
                }
            }
            else if (gameState == GameState.StayingInElevator)
            {
                DirUI.SetActive(false);
                // If right, go up a level
                // If wrong, restart
            }
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
            neutroAnim.SetBool("Selected", false);
            grotescoAnim.SetBool("Selected", false);
            maravilhaAnim.SetBool("Selected", true);
            elevador = Elevador.Maravilha;
        }
        else if (elevador != Elevador.Neutro && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            maravilhaAnim.SetBool("Selected", false);
            grotescoAnim.SetBool("Selected", false);
            neutroAnim.SetBool("Selected", true);
            elevador = Elevador.Neutro;
        }
        else if (elevador != Elevador.Grotesco && Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            neutroAnim.SetBool("Selected", false);
            maravilhaAnim.SetBool("Selected", false);
            grotescoAnim.SetBool("Selected", true);
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

    public void UpdateCameraAnimator(bool isWalking)
    {
        cameraAnim.SetBool("isWalking", isWalking);
    }


}
