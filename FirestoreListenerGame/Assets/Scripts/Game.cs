using System.Collections;
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
    public CameraController cameraController = null;
    public LightsController lightsController = null;
    public GameController gameController = null;

    public Player[] players = null;
    public Player currentPlayer = null;

    private enum GameState { chooseColour, randomLights, oneLight, play };
    private GameState gameState = GameState.chooseColour;

    private enum PlayState { waitForBox, dropBox, interactBox };
    private PlayState playState = PlayState.dropBox;

    private float timer = 0.0f;

    // Random lights
    public float lightsSeconds = 0.0f;

    // One light
    public float lightSeconds = 0.0f;

    // Wait for box
    public float waitForBoxSeconds = 0.0f;

    // Interact box
    public uint rotations = 0;

    void Start()
    {
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
    }

    void Update()
    {
        timer += Time.deltaTime;

        switch (gameState)
        {
            case GameState.chooseColour:

                // TODO: choose character

                if (timer >= 2.0f)
                {
                    timer = 0.0f;
                    lightsController.LightsOn();

                    gameState = GameState.randomLights;
                }

                break;

            case GameState.randomLights:

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

                if (timer >= lightSeconds)
                {
                    timer = 0.0f;

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

                    gameState = GameState.play;
                    playState = PlayState.waitForBox;
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
            case PlayState.waitForBox:

                if (timer >= waitForBoxSeconds)
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

                break;

            case PlayState.interactBox:

                if (!gameController.box_spawned)
                {
                    //box.can = true;
                    //box: una volta feta
                }

                break;
        }
    }
}
