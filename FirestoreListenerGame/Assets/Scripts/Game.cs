﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public bool active = true;
    public uint rotations = 0;

    public enum CurrentCamera { a, b, c, d, e };
    public CurrentCamera currentCamera = CurrentCamera.a;

    public enum CurrentPlayer { p1, p2, p3, p4 };
    public CurrentPlayer currentPlayer = CurrentPlayer.p1;
}

public class Game : MonoBehaviour
{
    //UI
    public GameObject manivela_UI;
    Animator manivela_animator;


    public GameObject choose_color_lbl;
    public GameObject choose_color_timer_lbl;
    float choose_color_timer = 10.0f;

    public CameraController cameraController = null;
    public LightsController lightsController = null;
    public GameController gameController = null;
    public Box box = null;

    public Player[] players = null;
    public Player currentPlayer = null;
    public Player nextPlayer = null;
    public Player lastPlayer = null;

    public enum GameState { chooseColour, randomLights, oneLight, play };
    public GameState gameState = GameState.chooseColour;

    public enum PlayState {
        waitLightOn, lightOn,
        waitDropBox, dropBox,
        interactBox,
        waitDie, die,
        waitLightOff, lightOff,
        waitMoveCamera, moveCamera
    };
    public PlayState playState = PlayState.dropBox;

    public uint numPlayers = 0;

    private float timer = 0.0f;

    // Random lights
    public float lightsSeconds = 0.0f;

    // One light
    public float lightSeconds = 0.0f;

    // Wait light on
    public float waitLightOnSeconds = 0.0f;

    // Wait drop box
    public float waitDropBoxSeconds = 0.0f;

    // Wait light off
    public float waitLightOffSeconds = 0.0f;

    // Wait move camera
    public float waitMoveCameraSeconds = 0.0f;

    void Start()
    {
        manivela_animator = manivela_UI.GetComponent<Animator>();

        players = new Player[4];

        for (uint i = 0; i < 4; ++i)
            players[i] = new Player();

        players[0].currentPlayer = Player.CurrentPlayer.p1;
        players[0].currentCamera = Player.CurrentCamera.a;
        players[1].currentPlayer = Player.CurrentPlayer.p2;
        players[1].currentCamera = Player.CurrentCamera.a;
        players[2].currentPlayer = Player.CurrentPlayer.p3;
        players[2].currentCamera = Player.CurrentCamera.a;
        players[3].currentPlayer = Player.CurrentPlayer.p4;
        players[3].currentCamera = Player.CurrentCamera.a;

        currentPlayer = nextPlayer = players[0];

        numPlayers = 4; // TODO: set num players
    }

    void Update()
    {
        if(box.can){
            manivela_animator.SetBool("fade_in_manivela", true);
        }else{
            manivela_animator.SetBool("fade_in_manivela", false);
        }

        choose_color_timer -= Time.deltaTime;

        switch (gameState)
        {
            case GameState.chooseColour:

                // TODO: choose character
                // TODO: FOLLOW THIS
                //choose_color_timer_lbl.GetComponent<GUIText>()
                timer += Time.deltaTime;

                if (choose_color_timer <= 0.0f)
                {
                    timer = 0.0f;
                    lightsController.LightsOn();

                    gameState = GameState.randomLights;
                }

                break;

            case GameState.randomLights:

                timer += Time.deltaTime;

                if (timer >= lightsSeconds)
                {
                    timer = 0.0f;

                    lightsController.LightsOff();

                    int random = Random.Range(0, 4);
                    switch (random)
                    {
                        case 0:
                            currentPlayer = players[0];
                            break;
                        case 1:
                            currentPlayer = players[1];
                            break;
                        case 2:
                            currentPlayer = players[2];
                            break;
                        case 3:
                            currentPlayer = players[3];
                            break;
                    }

                    lightsController.LightOn(currentPlayer.currentPlayer);                   
                    
                    gameState = GameState.oneLight;
                }

                break;

            case GameState.oneLight:

                timer += Time.deltaTime;

                if (timer >= lightSeconds)
                {
                    timer = 0.0f;

                    cameraController.animator.SetBool("toScene", false);

                    switch (currentPlayer.currentPlayer)
                    {
                        case Player.CurrentPlayer.p1:
                            cameraController.animator.SetBool("to1", true);
                            break;
                        case Player.CurrentPlayer.p2:
                            cameraController.animator.SetBool("to2", true);
                            break;
                        case Player.CurrentPlayer.p3:
                            cameraController.animator.SetBool("to3", true);
                            break;
                        case Player.CurrentPlayer.p4:
                            cameraController.animator.SetBool("to4", true);
                            break;
                    }

                    box.beat = true;
                    gameState = GameState.play;
                    playState = PlayState.waitLightOn;
                }

                break;

            case GameState.play:

                Play();

                break;
        }
	}

    void Play()
    {
        switch (playState)
        {
            case PlayState.waitLightOn:

                timer += Time.deltaTime;              

                if (timer >= waitLightOnSeconds)
                {
                    if (!currentPlayer.active)
                    {
                        Debug.Log("The player has died. The camera will move to the next player...");

                        switch (currentPlayer.currentPlayer)
                        {
                            case Player.CurrentPlayer.p1:
                                if (lastPlayer.currentPlayer == Player.CurrentPlayer.p4)
                                    nextPlayer = players[1];
                                else if (lastPlayer.currentPlayer == Player.CurrentPlayer.p2)
                                    nextPlayer = players[3];
                                break;
                            case Player.CurrentPlayer.p2:
                                if (lastPlayer.currentPlayer == Player.CurrentPlayer.p3)
                                    nextPlayer = players[0];
                                else if (lastPlayer.currentPlayer == Player.CurrentPlayer.p1)
                                    nextPlayer = players[2];
                                break;
                            case Player.CurrentPlayer.p3:
                                if (lastPlayer.currentPlayer == Player.CurrentPlayer.p4)
                                    nextPlayer = players[1];
                                else if (lastPlayer.currentPlayer == Player.CurrentPlayer.p2)
                                    nextPlayer = players[3];
                                break;
                            case Player.CurrentPlayer.p4:
                                if (lastPlayer.currentPlayer == Player.CurrentPlayer.p3)
                                    nextPlayer = players[0];
                                else if (lastPlayer.currentPlayer == Player.CurrentPlayer.p1)
                                    nextPlayer = players[2];
                                break;
                        }

                        timer = 0.0f;

                        playState = PlayState.moveCamera;

                        break;
                    }

                    timer = 0.0f;

                    playState = PlayState.lightOn;
                }

                break;

            case PlayState.lightOn:

                lightsController.LightOn(currentPlayer.currentPlayer);

                playState = PlayState.waitDropBox;

                break;

            case PlayState.waitDropBox:

                timer += Time.deltaTime;

                if (timer >= waitDropBoxSeconds)
                {
                    timer = 0.0f;

                    playState = PlayState.dropBox;
                }

                break;

            case PlayState.dropBox:

                switch (currentPlayer.currentPlayer)
                {
                    case Player.CurrentPlayer.p1:
                        gameController.SpawnBoxInChair1();
                        break;
                    case Player.CurrentPlayer.p2:
                        gameController.SpawnBoxInChair2();
                        break;
                    case Player.CurrentPlayer.p3:
                        gameController.SpawnBoxInChair3();
                        break;
                    case Player.CurrentPlayer.p4:
                        gameController.SpawnBoxInChair4();
                        break;
                }

                playState = PlayState.interactBox;
                Debug.Log("You can interact with the BOX now");

                break;

            case PlayState.interactBox:

                // If the box is spawned...
                if (!gameController.box_spawned)
                {
                    // Can rotate box
                    box.can = true;

                    if (currentPlayer.rotations > 0)
                        // Can move camera
                        cameraController.can = true;
                }

                break;

            // Despawn box

            case PlayState.waitDie:

                playState = PlayState.die;

                break;

            case PlayState.die:

                timer += Time.deltaTime;

                if (timer >= 5.0f) // TODO: PS
                {
                    timer = 0.0f;

                    playState = PlayState.waitLightOff;
                }

                break;

            case PlayState.waitLightOff:

                Debug.Log("waitLightOff");

                box.StopVibration();
                box.StopShake();

                if (!gameController.box_despawned)
                {
                    timer += Time.deltaTime;

                    if (timer >= waitLightOffSeconds)
                    {
                        timer = 0.0f;

                        playState = PlayState.lightOff;
                    }
                }

                break;

            case PlayState.lightOff:

                Debug.Log("lightOff");

                lightsController.LightOff(currentPlayer.currentPlayer);

                playState = PlayState.waitMoveCamera;

                break;

            case PlayState.waitMoveCamera:

                timer += Time.deltaTime;

                if (timer >= waitMoveCameraSeconds)
                {
                    timer = 0.0f;

                    playState = PlayState.moveCamera;
                }

                break;

            case PlayState.moveCamera:

                Debug.Log("moveCamera");

                lastPlayer = currentPlayer;
                currentPlayer = nextPlayer;

                cameraController.ResetPlayerCamera(lastPlayer.currentPlayer);

                switch (currentPlayer.currentPlayer)
                {
                    case Player.CurrentPlayer.p1:
                        cameraController.animator.SetBool("to1", true);
                        Debug.Log("Current player = 1");
                        break;
                    case Player.CurrentPlayer.p2:
                        cameraController.animator.SetBool("to2", true);
                        Debug.Log("Current player = 2");
                        break;
                    case Player.CurrentPlayer.p3:
                        cameraController.animator.SetBool("to3", true);
                        Debug.Log("Current player = 3");
                        break;
                    case Player.CurrentPlayer.p4:
                        cameraController.animator.SetBool("to4", true);
                        Debug.Log("Current player = 4");
                        break;
                }

                playState = PlayState.waitLightOn;

                break;
        }
    }

    public bool AllPlayersDead()
    {
        uint count = 0;

        for (uint i = 0; i < 4; ++i)
        {
            if (!players[i].active)
                count++;
        }

        return count == numPlayers - 1;
    }
}