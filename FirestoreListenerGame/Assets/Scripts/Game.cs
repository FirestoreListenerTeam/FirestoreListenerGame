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

    public enum GameState { chooseColour, randomLights, oneLight, play };
    public GameState gameState = GameState.chooseColour;

    public enum PlayState {
        waitLightOn, lightOn,
        waitDropBox, dropBox,
        interactBox,
        waitLightOff, lightOff,
        waitMoveCamera, moveCamera
    };
    public PlayState playState = PlayState.dropBox;

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
    }

    void Update()
    {
        timer += Time.deltaTime;
        choose_color_timer -= Time.deltaTime;

        switch (gameState)
        {
            case GameState.chooseColour:

                // TODO: choose character
                // TODO: FOLLOW THIS
                //choose_color_timer_lbl.GetComponent<GUIText>()

                if (choose_color_timer <= 0.0f)
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

                    cameraController.animator.SetBool("toScene", false);

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

                if (!currentPlayer.active)
                {
                    nextPlayer = currentPlayer;

                    switch (currentPlayer.currentPlayer)
                    {
                        case Player.CurrentPlayer.p1:
                            if (nextPlayer.currentPlayer == Player.CurrentPlayer.p4)
                                currentPlayer = players[1];
                            else if (nextPlayer.currentPlayer == Player.CurrentPlayer.p2)
                                currentPlayer = players[3];
                            break;
                        case Player.CurrentPlayer.p2:
                            if (nextPlayer.currentPlayer == Player.CurrentPlayer.p3)
                                currentPlayer = players[0];
                            else if (nextPlayer.currentPlayer == Player.CurrentPlayer.p1)
                                currentPlayer = players[2];
                            break;
                        case Player.CurrentPlayer.p3:
                            if (nextPlayer.currentPlayer == Player.CurrentPlayer.p4)
                                currentPlayer = players[1];
                            else if (nextPlayer.currentPlayer == Player.CurrentPlayer.p2)
                                currentPlayer = players[3];
                            break;
                        case Player.CurrentPlayer.p4:
                            if (nextPlayer.currentPlayer == Player.CurrentPlayer.p3)
                                currentPlayer = players[0];
                            else if (nextPlayer.currentPlayer == Player.CurrentPlayer.p1)
                                currentPlayer = players[2];
                            break;
                    }

                    playState = PlayState.lightOn;
                    break;
                }

                if (timer >= waitLightOnSeconds)
                {
                    timer = 0.0f;

                    playState = PlayState.lightOn;
                }

                break;

            case PlayState.lightOn:

                lightsController.LightOn(currentPlayer.currentPlayer);

                playState = PlayState.waitDropBox;

                break;

            case PlayState.waitDropBox:

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

            case PlayState.waitLightOff:

                if (!gameController.box_despawned)
                {
                    if (timer >= waitLightOffSeconds)
                    {
                        timer = 0.0f;

                        playState = PlayState.lightOff;
                    }
                }

                break;

            case PlayState.lightOff:

                lightsController.LightOff(currentPlayer.currentPlayer);

                playState = PlayState.waitMoveCamera;

                break;

            case PlayState.waitMoveCamera:

                if (timer >= waitMoveCameraSeconds)
                {
                    timer = 0.0f;

                    playState = PlayState.moveCamera;
                }

                break;

            case PlayState.moveCamera:

                Player lastPlayer = currentPlayer;
                currentPlayer = nextPlayer;
                nextPlayer = lastPlayer;

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

                cameraController.ResetPlayerCamera(lastPlayer.currentPlayer);

                playState = PlayState.waitLightOn;

                break;
        }
    }
}