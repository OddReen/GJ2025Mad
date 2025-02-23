using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMusicsController : MonoBehaviour
{
    public static FMODMusicsController instance;

    public FMODUnity.EventReference music1;
    public FMODUnity.EventReference music2;

    private FMOD.Studio.EventInstance musicInstance;
    private bool isPaused = false;
    private bool isElevatorPaused = true;
    private bool hasSwitchedMusic = false;

    public FMODUnity.EventReference elevatorSound;
    private FMOD.Studio.EventInstance elevatorInstance;

    private void Start()
    {
        instance = this;
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(music1);
        musicInstance.start();
    }

    public void PauseMusic()
    {
        if (musicInstance.isValid())
        {
            musicInstance.setPaused(true);
            isPaused = true;
        }
    }

    public void ResumeMusic()
    {
        if (musicInstance.isValid())
        {
            if (isPaused && !hasSwitchedMusic)
            {
                musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                musicInstance.release();
                musicInstance = FMODUnity.RuntimeManager.CreateInstance(music2);
                musicInstance.start();
                hasSwitchedMusic = true;
            }
            else
            {
                musicInstance.setPaused(false);
            }
            isPaused = false;
        }
    }

    public void StopMusic()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
        }
    }

    public void ToggleElevatorMusic()
    {
        if (elevatorInstance.isValid())
        {
            if (isElevatorPaused)
            {
                elevatorInstance.setPaused(false);
            }
            else
            {
                elevatorInstance.setPaused(true);
            }
            isElevatorPaused = !isElevatorPaused;
        }
        else
        {
            elevatorInstance = FMODUnity.RuntimeManager.CreateInstance(elevatorSound);
            elevatorInstance.start();
            isElevatorPaused = false;
        }
    }
}
