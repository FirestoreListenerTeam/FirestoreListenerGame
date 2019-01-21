using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public CameraController cameraController = null;
    public LightsController lightsController = null;

    public enum CurrentPlayer { p1, p2, p3, p4 };
    public CurrentPlayer currentPlayer = CurrentPlayer.p1;

    private enum GameState { chooseColour, randomLights, oneLight, play };
    private GameState gameState = GameState.chooseColour;

    private float timer = 0.0f;

    // Choose colour

    // Random lights
    public float lightsSeconds = 0.0f;

    // One light
    public float lightSeconds = 0.0f;
	
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
                }

                break;

            case GameState.play:

                // TODO

                break;
        }
	}
}
