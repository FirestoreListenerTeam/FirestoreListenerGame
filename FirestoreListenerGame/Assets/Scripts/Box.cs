using UnityEngine;
using XInputDotNetPure;

public class Box : MonoBehaviour
{
    public Game game = null;

    public float maxToLoad = 100.0f;
    public float increaseLoadPerTick = 1.0f;
    public float heartBeatValue = 0.25f;
    public float heartBeatTimeStep = 2.0f;

    private float currentLoaded = 0.0f;

    #region HeartVibVars
    private float normalizedLoaded;
    private float normalizedTimeStepLoaded;
    float timerStepHeart = 0.0f;
    float timerHeartVibration = 0.0f;
    #endregion

    enum angles { oneEighth, twoEighth, threeEighth, fourEighth, fiveEighth,
                  sixEighth, sevenEighth, eightEighth, noAngle }

    angles currentAngle = angles.noAngle;
    
    int anglesCount = 0;

    public bool can = false;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

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

                if (angle >= 0.0f && angle < 45.0f && currentAngle != angles.oneEighth && (currentAngle == angles.noAngle || currentAngle == angles.eightEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.oneEighth;
                }
                else if (angle >= 45.0f && angle < 90.0f && currentAngle != angles.twoEighth && (currentAngle == angles.noAngle || currentAngle == angles.oneEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.twoEighth;
                }
                else if (angle >= 90.0f && angle < 135.0f && currentAngle != angles.threeEighth && (currentAngle == angles.noAngle || currentAngle == angles.twoEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.threeEighth;
                }
                else if (angle >= 135.0f && angle < 180.0f && currentAngle != angles.fourEighth && (currentAngle == angles.noAngle || currentAngle == angles.threeEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.fourEighth;
                }
                else if (angle >= 180.0f && angle < 225.0f && currentAngle != angles.fiveEighth && (currentAngle == angles.noAngle || currentAngle == angles.fourEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.fiveEighth;
                }
                else if (angle >= 225.0f && angle < 270.0f && currentAngle != angles.sixEighth && (currentAngle == angles.noAngle || currentAngle == angles.fiveEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.sixEighth;
                }
                else if (angle >= 270.0f && angle < 315.0 && currentAngle != angles.sevenEighth && (currentAngle == angles.noAngle || currentAngle == angles.sixEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.sevenEighth;
                }
                else if (angle >= 315.0f && angle < 360.0f && currentAngle != angles.eightEighth && (currentAngle == angles.noAngle || currentAngle == angles.sevenEighth))
                {
                    if (currentAngle != angles.noAngle)
                    {
                        IncreaseLoad();
                        anglesCount++;
                    }
                    currentAngle = angles.eightEighth;
                }
            }
            else // Set to default
            {
                currentAngle = angles.noAngle;
            }

            if (anglesCount == 8)
            {
                anglesCount = 0;

                game.currentPlayer.rotations++;
                if (game.currentPlayer.rotations == 1)
                    Debug.Log("You can interact with the CAMERA now");
            }
        }

        HeartVibration();
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
            }
            else if (timerHeartVibration >= heartBeatValue && timerHeartVibration <= (heartBeatValue * 2))
            {
                GamePad.SetVibration(0, normalizedLoaded, 0);
            }
            else
            {
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
        normalizedLoaded = currentLoaded * 1.0f / maxToLoad;
        normalizedTimeStepLoaded = currentLoaded * heartBeatTimeStep / maxToLoad;
        normalizedTimeStepLoaded = heartBeatTimeStep - normalizedTimeStepLoaded;
    }
}
