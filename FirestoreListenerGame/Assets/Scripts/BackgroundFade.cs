﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFade : MonoBehaviour
{
    public Game game;
    public Box box;

    public Image red; // 0
    public Image blue; // 1
    public Image green; // 2
    public Image yellow; // 3

    public float speed = 1.0f;
    float realSpeed = 0.0f;

    bool playFade = false;
    bool halfFade = false;
	
	void Update()
    {
        if (playFade)
        {
            Image image = null;
            switch (game.currentPlayer.currentPlayer)
            {
                case Player.CurrentPlayer.p1:
                    image = green;
                    break;
                case Player.CurrentPlayer.p2:
                    image = red;
                    break;
                case Player.CurrentPlayer.p3:
                    image = yellow;
                    break;
                case Player.CurrentPlayer.p4:
                    image = blue;
                    break;
            }

            if (!halfFade)
            {
                realSpeed = speed * box.normalizedLoaded;
                Color color = image.color;
                color.a += Time.deltaTime * realSpeed;

                if (color.a >= box.normalizedLoaded)
                {
                    color.a = box.normalizedLoaded;
                    halfFade = true;
                }

                image.color = color;
            }
            else
            {
                realSpeed = speed * box.normalizedLoaded;
                Color color = image.color;
                color.a -= Time.deltaTime * realSpeed;

                if (color.a <= 0.0f)
                {
                    color.a = 0.0f;
                    playFade = false;
                }

                image.color = color;
            }
        }

	}

    public void PlayFade()
    {
        playFade = true;
        halfFade = false;
    }

    public void ClearFade()
    {
        Image image = null;
        switch (game.currentPlayer.currentPlayer)
        {
            case Player.CurrentPlayer.p1:
                image = green;
                break;
            case Player.CurrentPlayer.p2:
                image = red;
                break;
            case Player.CurrentPlayer.p3:
                image = yellow;
                break;
            case Player.CurrentPlayer.p4:
                image = blue;
                break;
        }

        Color color = image.color;
        color.a = 0.0f;
        image.color = color;
    }
}