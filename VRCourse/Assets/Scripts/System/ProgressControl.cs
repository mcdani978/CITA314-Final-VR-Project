using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ProgressControl : MonoBehaviour
{
    public UnityEvent<string> OnStartGame;
    public UnityEvent<string> OnChallengeComplete;


    [Header("Start Button")]
    [SerializeField]
    ButtonInteractable buttonInteractable;

    [Header("Drawer Interactables")]

    [SerializeField]
    DrawerInteractable drawer;

    XRSocketInteractor drawerSocket;

    [SerializeField]
    CombinationLock comboLock;

    [Header("Challenge Settings")]
    [SerializeField]
    GameObject keyInteractableLight;

    [SerializeField]
    string startGameString;

    [SerializeField]
    string[] challengeStrings;


    [Header("The Wall")]

    [SerializeField]
    TheWall wall;

    XRSocketInteractor wallSocket;

    [SerializeField]
    GameObject teleportationAreas;
    bool startGame;
    [SerializeField]
    int challengeNum;

    [Header("Library")]
    [SerializeField]
    SimpleSliderControl librarySlider;

    [Header("The robot")]
    [SerializeField]
    NavMeshRobot robot;


    [SerializeField]
    int wallCubesToDestroy;

    int wallCubesDestroyed;

    bool challengesCompleted;

    [SerializeField]
    string endGameString;

    //Called before first frame update
    void Start()
    {
        //Tell button interactable to list to method when it is selected
        if (buttonInteractable != null)
        {
            buttonInteractable.selectEntered.AddListener(ButtonInteractablePressed);
        }

        OnStartGame?.Invoke(startGameString);
        SetDrawerInteractable();

        if (comboLock != null)
        {
            comboLock.UnlockAction += OnComboUnlocked;
        }

        if (wall != null)
        {
            SetWall();
        }
        if (librarySlider != null)
        {
            librarySlider.OnSliderActive.AddListener(LibrarySliderActive);
        }
        if (robot != null)
        {
            robot.OnDestroyWallCube.AddListener(OnDestroyWallCube);
        }
    }

    private void OnDestroyWallCube()
    {
        wallCubesDestroyed++;
        if (wallCubesDestroyed >= wallCubesToDestroy && !challengesCompleted)
        {
            challengesCompleted = true;
            if (challengeNum == 6)
            {
                ChallengeComplete();
            }
        }
    }

    private void LibrarySliderActive()
    {
        if (challengeNum == 5)
        {
            ChallengeComplete();
        }
    }

    private void SetWall()
    {
        wall.OnDestroy.AddListener(OnDestroyWall);

        wallSocket = wall.GetWallSocket;
        if (wallSocket != null)
        {
            wallSocket.selectEntered.AddListener(OnWallSocketed);
        }
    }

    private void OnWallSocketed(SelectEnterEventArgs arg0)
    {
        if (challengeNum == 3)
        {
            ChallengeComplete();
        }
    }

    private void OnDestroyWall()
    {
        if (challengeNum == 4)
        {
            ChallengeComplete();
        }
        if (teleportationAreas != null)
        {
            teleportationAreas.SetActive(true);
        }
    }

    private void OnComboUnlocked()
    {
        if (challengeNum == 2)
        {
            ChallengeComplete();
        }
    }

    //Method for when button is pressed
    void ButtonInteractablePressed(SelectEnterEventArgs arg0)
    {
        if (!startGame)
        {
            startGame = true;
            //Temportaty array assignment
            if (keyInteractableLight != null)
            {
                keyInteractableLight.SetActive(true);
            }

            if (challengeNum < challengeStrings.Length && challengeNum == 0)
            {
                OnStartGame?.Invoke(challengeStrings[challengeNum]);
            }
        }


    }

    void ChallengeComplete()
    {
        challengeNum++;
        if (challengeNum < challengeStrings.Length)
        {
            OnChallengeComplete?.Invoke(challengeStrings[challengeNum]);
        }
        else if (challengeNum >= challengeStrings.Length)
        {
            //ALL CHALLENGES COMPLETE
            OnChallengeComplete?.Invoke(endGameString);
        }
    }

    void SetDrawerInteractable()
    {
        if (drawer != null)
        {
            drawer.OnDrawerDetatch.AddListener(OnDrawerDetatch);


            drawerSocket = drawer.GetSocketIntractor;
            if (drawerSocket != null)
            {
                drawerSocket.selectEntered.AddListener(OnDrawerSocketed);
            }
        }
}

    private void OnDrawerDetatch()
    {
        if (challengeNum == 1)
        {
            ChallengeComplete();
        }
    }

    private void OnDrawerSocketed(SelectEnterEventArgs arg0)
    {
        if (challengeNum == 0)
        {
            ChallengeComplete();

        }
    }
}
