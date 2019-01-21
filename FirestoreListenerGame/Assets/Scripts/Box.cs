using UnityEngine;
using XInputDotNetPure;

public class Box : MonoBehaviour
{

    public float maxToLoad = 100.0f;
    public float heartBeatValue = 0.25f;
    public float heartBeatTimeStep = 2.0f;

    private float currentLoaded = 0.0f;

    #region HeartVibVars
    private float normalizedLoaded;
    private float normalizedTimeStepLoaded;
    float timerStepHeart = 0.0f;
    float timerHeartVibration = 0.0f;
    #endregion

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    void Update()
    {

        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            currentLoaded += 5.0f;

            // normalized values
            normalizedLoaded = currentLoaded * 1.0f / maxToLoad;
            normalizedTimeStepLoaded = currentLoaded * heartBeatTimeStep / maxToLoad;
            normalizedTimeStepLoaded = heartBeatTimeStep - normalizedTimeStepLoaded;
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
}
