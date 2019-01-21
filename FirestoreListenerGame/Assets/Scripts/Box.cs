using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Box : MonoBehaviour {

    float maxToLoad = 100.0f;
    public float perCentLoaded = 0.0f;
    public float normalizedLoaded;

    float timerHearth = 0.0f;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    void Update () {

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
            perCentLoaded += 5.0f;
            normalizedLoaded = perCentLoaded * 1.0f / maxToLoad;
        }

        timerHearth += 1.0f * Time.deltaTime;

        if (timerHearth > 1.0f)
        {
            GamePad.SetVibration(0, normalizedLoaded, normalizedLoaded);
            timerHearth = 0.0f;
        }     

    }
}
