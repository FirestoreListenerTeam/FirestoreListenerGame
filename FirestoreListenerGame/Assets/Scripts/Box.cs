using UnityEngine;
using XInputDotNetPure;

public class Box : MonoBehaviour
{
    bool moving = false;

    public Game game = null;
    public CameraController cameraController = null;
    public CameraShake cameraShake = null;
    public GameController gameController = null;

    public float minToLoad, maxToLoad = 0.0f;
    private float currentToLoad = 100.0f;
    
    public GameObject clank;
    
    public float increaseLoadPerTick = 1.0f;
    public float crankCooldown = 1.0f;
    public float heartBeatValue = 0.25f;
    public float heartBeatTimeStep = 2.0f;

    int countClank = 0;
    int currentZone = 0, previousZone = 0;

    private float currentLoaded = 0.0f;
    
    float prevIncreasedAngle;
    public float offsetAngleDegrees = 2.5f;

    bool firstUpdateAxis = false;

    #region HeartVibVars
    private float normalizedLoaded;
    private float normalizedTimeStepLoaded;
    float timerStepHeart = 0.0f;
    float timerHeartVibration = 0.0f;
    #endregion

    bool heartBeatPlayed = false;
    bool heartBeat2Played = false;
    public AudioClip heartBeat1, heartBeat2;
    public AudioSource source;

    enum angles { oneEighth, twoEighth, threeEighth, fourEighth, fiveEighth,
                  sixEighth, sevenEighth, eightEighth, noAngle }

    angles currentAngle = angles.noAngle;
    
    int anglesCount = 0;

    public bool can = false;
    public bool beat = false;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    void Start()
    {
        currentToLoad = Random.Range(minToLoad, maxToLoad);
        Debug.Log("Next currentToLoad: " + currentToLoad);
    }

    void Update()
    {
        if (can)
        {
            if (!playerIndexSet || !prevState.IsConnected)
            {
                for (int i = 0; i < 4; ++i)
                {
                    PlayerIndex testPlayerIndex = (PlayerIndex)i;
                    GamePadState testState = GamePad.GetState(testPlayerIndex);
                    if (testState.IsConnected)
                    {
                        //Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                        playerIndex = testPlayerIndex;
                        playerIndexSet = true;
                    }
                }
            }

            prevState = state;
            state = GamePad.GetState(playerIndex);

            // Thumbstick tick
            if (state.ThumbSticks.Right.X > 0.0f ||
                state.ThumbSticks.Right.X < 0.0f ||
                state.ThumbSticks.Right.Y > 0.0f ||
                state.ThumbSticks.Right.Y < 0.0f)
            {
                float angle = FindDegree(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);

                moving = true;
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///

                if (angle >= 0.0f && angle < 45.0f && currentAngle != angles.oneEighth && (currentAngle == angles.noAngle || currentAngle == angles.eightEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        currentZone = 1;
                    }
                }
                else if (angle >= 45.0f && angle < 90.0f && currentAngle != angles.twoEighth && (currentAngle == angles.noAngle || currentAngle == angles.oneEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        currentZone = 1;
                    }
                }
                else if (angle >= 90.0f && angle < 135.0f && currentAngle != angles.threeEighth && (currentAngle == angles.noAngle || currentAngle == angles.twoEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        currentZone = 1;
                    }
                }
                else if (angle >= 135.0f && angle < 180.0f && currentAngle != angles.fourEighth && (currentAngle == angles.noAngle || currentAngle == angles.threeEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        currentZone = 1;
                    }
                }
                else if (angle >= 180.0f && angle < 225.0f && currentAngle != angles.fiveEighth && (currentAngle == angles.noAngle || currentAngle == angles.fourEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        currentZone = 0;
                    }
                }
                else if (angle >= 225.0f && angle < 270.0f && currentAngle != angles.sixEighth && (currentAngle == angles.noAngle || currentAngle == angles.fiveEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        currentZone = 0;
                    }
                }
                else if (angle >= 270.0f && angle < 315.0 && currentAngle != angles.sevenEighth && (currentAngle == angles.noAngle || currentAngle == angles.sixEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        currentZone = 0;
                    }
                }
                else if (angle >= 315.0f && angle < 360.0f && currentAngle != angles.eightEighth && (currentAngle == angles.noAngle || currentAngle == angles.sevenEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        currentZone = 0;
                    }
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////7
                // Clank rotation

                if (angle >= prevIncreasedAngle + offsetAngleDegrees || (currentZone == 1 && previousZone == 0))
                {
                    float toIncrease = 0.0f;
                    if (firstUpdateAxis)
                        toIncrease = angle - prevIncreasedAngle;
                    prevIncreasedAngle = angle;

                    Quaternion xRotation = clank.transform.rotation;

                    xRotation = Quaternion.AngleAxis(-toIncrease, clank.transform.up) * xRotation;
                    clank.transform.rotation = xRotation;
                }
                //--------

                if (angle >= 0.0f && angle < 45.0f && currentAngle != angles.oneEighth && (currentAngle == angles.noAngle || currentAngle == angles.eightEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        previousZone = 1;
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.oneEighth;
                }
                else if (angle >= 45.0f && angle < 90.0f && currentAngle != angles.twoEighth && (currentAngle == angles.noAngle || currentAngle == angles.oneEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        previousZone = 1;
                        IncreaseLoad();
                        anglesCount++;
                    }   
                    currentAngle = angles.twoEighth;
                }
                else if (angle >= 90.0f && angle < 135.0f && currentAngle != angles.threeEighth && (currentAngle == angles.noAngle || currentAngle == angles.twoEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        previousZone = 1;
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.threeEighth;
                }
                else if (angle >= 135.0f && angle < 180.0f && currentAngle != angles.fourEighth && (currentAngle == angles.noAngle || currentAngle == angles.threeEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        previousZone = 1;
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.fourEighth;
                }
                else if (angle >= 180.0f && angle < 225.0f && currentAngle != angles.fiveEighth && (currentAngle == angles.noAngle || currentAngle == angles.fourEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        previousZone = 0;
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.fiveEighth;
                }
                else if (angle >= 225.0f && angle < 270.0f && currentAngle != angles.sixEighth && (currentAngle == angles.noAngle || currentAngle == angles.fiveEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        previousZone = 0;
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.sixEighth;
                }
                else if (angle >= 270.0f && angle < 315.0 && currentAngle != angles.sevenEighth && (currentAngle == angles.noAngle || currentAngle == angles.sixEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        previousZone = 0;
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.sevenEighth;
                }
                else if (angle >= 315.0f && angle < 360.0f && currentAngle != angles.eightEighth && (currentAngle == angles.noAngle || currentAngle == angles.sevenEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        previousZone = 0;
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.eightEighth;
                }
                
                firstUpdateAxis = true;
            }
            else // Set to default
            {
                firstUpdateAxis = false;
                currentAngle = angles.noAngle;
                currentZone = 0;
                moving = false;
            }
            

            if (anglesCount == 8)
            {
                anglesCount = 0;

                game.currentPlayer.rotations++;

                if (game.currentPlayer.rotations < 5)
                {
                    if (game.currentPlayer.rotations == 1)
                        Debug.Log("You can interact with the CAMERA now");

                    // Move the camera close
                    switch (game.currentPlayer.currentPlayer)
                    {
                        case Player.CurrentPlayer.p1:
                            switch (game.currentPlayer.currentCamera)
                            {
                                case Player.CurrentCamera.a:
                                    cameraController.animator.SetBool("to1b", true);
                                    Debug.Log("to1b");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.b;
                                    break;
                                case Player.CurrentCamera.b:
                                    cameraController.animator.SetBool("to1c", true);
                                    Debug.Log("to1c");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.c;
                                    break;
                                case Player.CurrentCamera.c:
                                    cameraController.animator.SetBool("to1d", true);
                                    Debug.Log("to1d");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.d;
                                    break;
                                case Player.CurrentCamera.d:
                                    cameraController.animator.SetBool("to1e", true);
                                    Debug.Log("to1e");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.e;
                                    break;
                            }
                            break;
                        case Player.CurrentPlayer.p2:
                            switch (game.currentPlayer.currentCamera)
                            {
                                case Player.CurrentCamera.a:
                                    cameraController.animator.SetBool("to2b", true);
                                    Debug.Log("to2b");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.b;
                                    break;
                                case Player.CurrentCamera.b:
                                    cameraController.animator.SetBool("to2c", true);
                                    Debug.Log("to2c");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.c;
                                    break;
                                case Player.CurrentCamera.c:
                                    cameraController.animator.SetBool("to2d", true);
                                    Debug.Log("to2d");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.d;
                                    break;
                                case Player.CurrentCamera.d:
                                    cameraController.animator.SetBool("to2e", true);
                                    Debug.Log("to2e");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.e;
                                    break;
                            }
                            break;
                        case Player.CurrentPlayer.p3:
                            switch (game.currentPlayer.currentCamera)
                            {
                                case Player.CurrentCamera.a:
                                    cameraController.animator.SetBool("to3b", true);
                                    Debug.Log("to3b");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.b;
                                    break;
                                case Player.CurrentCamera.b:
                                    cameraController.animator.SetBool("to3c", true);
                                    Debug.Log("to3c");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.c;
                                    break;
                                case Player.CurrentCamera.c:
                                    cameraController.animator.SetBool("to3d", true);
                                    Debug.Log("to3d");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.d;
                                    break;
                                case Player.CurrentCamera.d:
                                    cameraController.animator.SetBool("to3e", true);
                                    Debug.Log("to3e");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.e;
                                    break;
                            }
                            break;
                        case Player.CurrentPlayer.p4:
                            switch (game.currentPlayer.currentCamera)
                            {
                                case Player.CurrentCamera.a:
                                    cameraController.animator.SetBool("to4b", true);
                                    Debug.Log("to4b");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.b;
                                    break;
                                case Player.CurrentCamera.b:
                                    cameraController.animator.SetBool("to4c", true);
                                    Debug.Log("to4c");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.c;
                                    break;
                                case Player.CurrentCamera.c:
                                    cameraController.animator.SetBool("to4d", true);
                                    Debug.Log("to4d");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.d;
                                    break;
                                case Player.CurrentCamera.d:
                                    cameraController.animator.SetBool("to4e", true);
                                    Debug.Log("to4e");
                                    game.currentPlayer.currentCamera = Player.CurrentCamera.e;
                                    break;
                            }
                            break;
                    }                 
                }
                else
                {
                    cameraShake.Shake(game.currentPlayer, 1.0f, 0.5f, 5.0f);
                    Debug.Log("Shake!");
                }
            }
        }

        //if (beat)
            HeartVibration();
    }

    public void MaxShake()
    {
        cameraShake.Shake(game.currentPlayer, 5.0f, 1.0f, 10.0f);
        Debug.Log("Max shake!");
    }

    public void StopShake()
    {
        cameraShake.StopShake();
        Debug.Log("Stop shake!");
    }

    void HeartVibration()
    {
        timerStepHeart += 1.0f * Time.deltaTime;

        if (timerStepHeart > normalizedTimeStepLoaded)
        {
            timerHeartVibration += 1.0f * Time.deltaTime;

            if (timerHeartVibration < heartBeatValue)
            {
                GamePad.SetVibration(0, 0, normalizedLoaded);
                if (!heartBeatPlayed)
                {
                    source.PlayOneShot(heartBeat1);
                    heartBeatPlayed = true;
                }
            }
            else if (timerHeartVibration >= heartBeatValue && timerHeartVibration <= (heartBeatValue * 2))
            {
                GamePad.SetVibration(0, normalizedLoaded, 0);
                if (!heartBeat2Played)
                {
                    source.PlayOneShot(heartBeat2);
                    heartBeat2Played = true;
                }
            }
            else
            {
                heartBeatPlayed = false;
                heartBeat2Played = false;
                GamePad.SetVibration(0, 0, 0);
                timerStepHeart = 0.0f;
                timerHeartVibration = 0.0f;
            }
        }
    }

    public static float FindDegree(float x, float y)
    {
        float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
        if (value < 0) value += 360f;

        return value;
    }

    public void IncreaseLoad()
    {
        currentLoaded += increaseLoadPerTick;

        // normalized values
        normalizedLoaded = currentLoaded * 1.0f / currentToLoad;
        normalizedTimeStepLoaded = currentLoaded * heartBeatTimeStep / currentToLoad;
        normalizedTimeStepLoaded = heartBeatTimeStep - normalizedTimeStepLoaded;

        // Explode?
        if (currentLoaded >= maxToLoad)
        {
            currentLoaded = 0.0f;
            normalizedLoaded = 0.0f;
            normalizedTimeStepLoaded = 0.0f;
            currentToLoad = Random.Range(minToLoad, maxToLoad);

            Debug.Log("You died! Next currentToLoad: " + currentToLoad);

            SetMaxVibration();
            MaxShake();

            // Reset variables
            switch (game.currentPlayer.currentPlayer)
            {
                case Player.CurrentPlayer.p1:
                    game.nextPlayer = game.players[1];
                    break;
                case Player.CurrentPlayer.p2:
                    game.nextPlayer = game.players[2];
                    break;
                case Player.CurrentPlayer.p3:
                    game.nextPlayer = game.players[3];
                    break;
                case Player.CurrentPlayer.p4:
                    game.nextPlayer = game.players[0];
                    break;
            }

            cameraController.can = false;
            can = false;
            gameController.DespawnBox();
            game.currentPlayer.rotations = 0;
            game.currentPlayer.currentCamera = Player.CurrentCamera.a;
            game.currentPlayer.active = false;
            game.playState = Game.PlayState.waitDie;

            // Play again?
            if (game.AllPlayersDead())
            {
                Debug.Log("All players died...");
                // TODO: end of the game
            }
        }
    }

    public void SetMaxVibration()
    {
        GamePad.SetVibration(0, 1.0f, 1.0f);
    }

    public void StopVibration()
    {
        GamePad.SetVibration(0, 0.0f, 0.0f);
    }
}
