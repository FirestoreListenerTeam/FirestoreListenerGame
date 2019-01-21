using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public CameraController cameraController = null;
    public LightsController lightsController = null;

    public enum CurrentPlayer { p1, p2, p3, p4 };
    public CurrentPlayer currentPlayer = CurrentPlayer.p1;

    private enum GameState { chooseColour, randomLights, oneLight };
    private GameState gameState = GameState.chooseColour;

    // Choose colour

    // Random lights
    public float lightsSeconds = 0.0f;
    private float timer = 0.0f;   
	
	void Update()
    {
        timer += Time.deltaTime;

        switch (gameState)
        {
            case GameState.chooseColour:

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

                break;
        }
	}
}
