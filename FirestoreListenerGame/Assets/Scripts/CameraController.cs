using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CameraController : MonoBehaviour
{
    public Game game = null;
    public Animator animator = null;

    public bool can = false;

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

        if (can)
        {
            // Detect if a button was pressed this frame
            if ((prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
                || (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed))
            {
                switch (game.currentPlayer)
                {
                    case Game.CurrentPlayer.p1:
                        game.currentPlayer = Game.CurrentPlayer.p2;
                        animator.SetBool("to1", false);
                        animator.SetBool("to2", true);
                        animator.SetBool("to3", false);
                        animator.SetBool("to4", false);
                        break;
                    case Game.CurrentPlayer.p2:
                        game.currentPlayer = Game.CurrentPlayer.p3;
                        animator.SetBool("to1", false);
                        animator.SetBool("to2", false);
                        animator.SetBool("to3", true);
                        animator.SetBool("to4", false);
                        break;
                    case Game.CurrentPlayer.p3:
                        game.currentPlayer = Game.CurrentPlayer.p4;
                        animator.SetBool("to1", false);
                        animator.SetBool("to2", false);
                        animator.SetBool("to3", false);
                        animator.SetBool("to4", true);
                        break;
                    case Game.CurrentPlayer.p4:
                        game.currentPlayer = Game.CurrentPlayer.p1;
                        animator.SetBool("to1", true);
                        animator.SetBool("to2", false);
                        animator.SetBool("to3", false);
                        animator.SetBool("to4", false);
                        break;
                }
            }
        }
    }
}