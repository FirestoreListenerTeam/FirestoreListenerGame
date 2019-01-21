using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public CameraController cameraController = null;
    public LightsController lightsController = null;
    public GameController gameController = null;

    public enum CurrentPlayer { p1, p2, p3, p4 };
    public CurrentPlayer currentPlayer = CurrentPlayer.p1;

    private enum GameState { chooseColour, randomLights, oneLight, play };
    private GameState gameState = GameState.chooseColour;

    private enum PlayState { dropBox, interactBox };
    private PlayState playState = PlayState.dropBox;

    private float timer = 0.0f;

    // Random lights
    public float lightsSeconds = 0.0f;

    // One light
    public float lightSeconds = 0.0f;

    // Interact box
    public uint rotations = 0;
	
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
                            currentPlayer = CurrentPlayer.p1;
                            break;
                        case 1:
                            currentPlayer = CurrentPlayer.p2;
                            break;
                        case 2:
                            currentPlayer = CurrentPlayer.p3;
                            break;
                        case 3:
                            currentPlayer = CurrentPlayer.p4;
                            break;
                    }

                    lightsController.LightOn(currentPlayer);                   
                    
                    gameState = GameState.oneLight;
                }

                break;

            case GameState.oneLight:

                if (timer >= lightSeconds)
                {
                    timer = 0.0f;

                    switch (currentPlayer)
                    {
                        case CurrentPlayer.p1:
                            cameraController.animator.SetBool("to1", true);
                            break;
                        case CurrentPlayer.p2:
                            cameraController.animator.SetBool("to2", true);
                            break;
                        case CurrentPlayer.p3:
                            cameraController.animator.SetBool("to3", true);
                            break;
                        case CurrentPlayer.p4:
                            cameraController.animator.SetBool("to4", true);
                            break;
                    }

                    gameState = GameState.play;
                    playState = PlayState.dropBox;
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
            case PlayState.dropBox:

                switch (currentPlayer)
                {
                    case CurrentPlayer.p1:
                        gameController.SpawnBoxInChair1();
                        break;
                    case CurrentPlayer.p2:
                        gameController.SpawnBoxInChair2();
                        break;
                    case CurrentPlayer.p3:
                        gameController.SpawnBoxInChair3();
                        break;
                    case CurrentPlayer.p4:
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
