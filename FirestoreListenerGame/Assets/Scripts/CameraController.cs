using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CameraController : MonoBehaviour
{
    private Animator animator = null;
    private enum CurrentPlayer { p1, p2, p3, p4 };
    private CurrentPlayer currentPlayer = CurrentPlayer.p1;

    // Controller
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    void Start()
    {
        animator = GetComponent<Animator>();
	}
	
	void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
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

        // Start

        // Detect if a button was pressed this frame
        if ((prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
            || (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed))
        {
            switch (currentPlayer)
            {
                case CurrentPlayer.p1:
                    currentPlayer = CurrentPlayer.p2;
                    animator.SetBool("to1", false);
                    animator.SetBool("to2", true);
                    animator.SetBool("to3", false);
                    animator.SetBool("to4", false);
                    break;
                case CurrentPlayer.p2:
                    currentPlayer = CurrentPlayer.p3;
                    animator.SetBool("to1", false);
                    animator.SetBool("to2", false);
                    animator.SetBool("to3", true);
                    animator.SetBool("to4", false);
                    break;
                case CurrentPlayer.p3:
                    currentPlayer = CurrentPlayer.p4;
                    animator.SetBool("to1", false);
                    animator.SetBool("to2", false);
                    animator.SetBool("to3", false);
                    animator.SetBool("to4", true);
                    break;
                case CurrentPlayer.p4:
                    currentPlayer = CurrentPlayer.p1;
                    animator.SetBool("to1", true);
                    animator.SetBool("to2", false);
                    animator.SetBool("to3", false);
                    animator.SetBool("to4", false);
                    break;
            }
        }
    }
}