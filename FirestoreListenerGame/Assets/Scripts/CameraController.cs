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
                switch (game.currentPlayer.currentPlayer)
                {
                    case Player.CurrentPlayer.p1:
                        game.currentPlayer = game.players[1];
                        animator.SetBool("to1", false);
                        animator.SetBool("to2", true);
                        animator.SetBool("to3", false);
                        animator.SetBool("to4", false);
                        break;
                    case Player.CurrentPlayer.p2:
                        game.currentPlayer = game.players[2];
                        animator.SetBool("to1", false);
                        animator.SetBool("to2", false);
                        animator.SetBool("to3", true);
                        animator.SetBool("to4", false);
                        break;
                    case Player.CurrentPlayer.p3:
                        game.currentPlayer = game.players[3];
                        animator.SetBool("to1", false);
                        animator.SetBool("to2", false);
                        animator.SetBool("to3", false);
                        animator.SetBool("to4", true);
                        break;
                    case Player.CurrentPlayer.p4:
                        game.currentPlayer = game.players[0];
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