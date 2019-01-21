using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera1;
    public Cinemachine.CinemachineVirtualCamera virtualCamera2;
    public Cinemachine.CinemachineVirtualCamera virtualCamera3;
    public Cinemachine.CinemachineVirtualCamera virtualCamera4;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise1;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise2;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise3;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise4;

    public Cinemachine.CinemachineVirtualCamera virtualCamera1b;
    public Cinemachine.CinemachineVirtualCamera virtualCamera2b;
    public Cinemachine.CinemachineVirtualCamera virtualCamera3b;
    public Cinemachine.CinemachineVirtualCamera virtualCamera4b;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise1b;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise2b;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise3b;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise4b;

    public Cinemachine.CinemachineVirtualCamera virtualCamera1c;
    public Cinemachine.CinemachineVirtualCamera virtualCamera2c;
    public Cinemachine.CinemachineVirtualCamera virtualCamera3c;
    public Cinemachine.CinemachineVirtualCamera virtualCamera4c;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise1c;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise2c;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise3c;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise4c;

    public Cinemachine.CinemachineVirtualCamera virtualCamera1d;
    public Cinemachine.CinemachineVirtualCamera virtualCamera2d;
    public Cinemachine.CinemachineVirtualCamera virtualCamera3d;
    public Cinemachine.CinemachineVirtualCamera virtualCamera4d;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise1d;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise2d;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise3d;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise4d;

    public Cinemachine.CinemachineVirtualCamera virtualCamera1e;
    public Cinemachine.CinemachineVirtualCamera virtualCamera2e;
    public Cinemachine.CinemachineVirtualCamera virtualCamera3e;
    public Cinemachine.CinemachineVirtualCamera virtualCamera4e;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise1e;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise2e;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise3e;
    private Cinemachine.CinemachineBasicMultiChannelPerlin noise4e;

    private float duration = 0.0f;
    private float amplitude = 0.0f;
    private float frequency = 0.0f;

    public float timer = 0.0f;

    private Cinemachine.CinemachineBasicMultiChannelPerlin currentNoise;

    void Start()
    {
        if (virtualCamera1 != null)
            noise1 = virtualCamera1.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera2 != null)
            noise2 = virtualCamera2.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera3 != null)
            noise3 = virtualCamera3.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera4 != null)
            noise4 = virtualCamera4.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        if (virtualCamera1b != null)
            noise1b = virtualCamera1b.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera2b != null)
            noise2b = virtualCamera2b.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera3b != null)
            noise3b = virtualCamera3b.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera4b != null)
            noise4b = virtualCamera4b.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        if (virtualCamera1c != null)
            noise1c = virtualCamera1c.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera2c != null)
            noise2c = virtualCamera2c.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera3c != null)
            noise3c = virtualCamera3c.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera4c != null)
            noise4c = virtualCamera4c.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        if (virtualCamera1d != null)
            noise1d = virtualCamera1d.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera2d != null)
            noise2d = virtualCamera2d.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera3d != null)
            noise3d = virtualCamera3d.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera4d != null)
            noise4d = virtualCamera4d.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        if (virtualCamera1e != null)
            noise1e = virtualCamera1e.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera2e != null)
            noise2e = virtualCamera2e.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera3e != null)
            noise3e = virtualCamera3e.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        if (virtualCamera4e != null)
            noise4e = virtualCamera4e.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        if (currentNoise != null)
        {
            if (timer <= 0.0f)
            {
                timer = 0.0f;

                currentNoise.m_AmplitudeGain = 0.0f;
                currentNoise.m_FrequencyGain = 0.0f;

                currentNoise = null;
            }
            else
            {
                currentNoise.m_AmplitudeGain = amplitude;
                currentNoise.m_FrequencyGain = frequency;

                timer -= Time.deltaTime;
            }
        }
    }

    public void Shake(Player currentPlayer, float duration, float amplitude, float frequency)
    {
        this.duration = duration;
        this.amplitude = amplitude;
        this.frequency = frequency;

        timer = duration;

        switch (currentPlayer.currentPlayer)
        {
            case Player.CurrentPlayer.p1:
                switch (currentPlayer.currentCamera)
                {
                    case Player.CurrentCamera.a:
                        currentNoise = noise1;
                        break;
                    case Player.CurrentCamera.b:
                        currentNoise = noise1b;
                        break;
                    case Player.CurrentCamera.c:
                        currentNoise = noise1c;
                        break;
                    case Player.CurrentCamera.d:
                        currentNoise = noise1d;
                        break;
                    case Player.CurrentCamera.e:
                        currentNoise = noise1e;
                        break;
                }
                break;
            case Player.CurrentPlayer.p2:
                switch (currentPlayer.currentCamera)
                {
                    case Player.CurrentCamera.a:
                        currentNoise = noise2;
                        break;
                    case Player.CurrentCamera.b:
                        currentNoise = noise2b;
                        break;
                    case Player.CurrentCamera.c:
                        currentNoise = noise2c;
                        break;
                    case Player.CurrentCamera.d:
                        currentNoise = noise2d;
                        break;
                    case Player.CurrentCamera.e:
                        currentNoise = noise2e;
                        break;
                }
                break;
            case Player.CurrentPlayer.p3:
                switch (currentPlayer.currentCamera)
                {
                    case Player.CurrentCamera.a:
                        currentNoise = noise3;
                        break;
                    case Player.CurrentCamera.b:
                        currentNoise = noise3b;
                        break;
                    case Player.CurrentCamera.c:
                        currentNoise = noise3c;
                        break;
                    case Player.CurrentCamera.d:
                        currentNoise = noise3d;
                        break;
                    case Player.CurrentCamera.e:
                        currentNoise = noise3e;
                        break;
                }
                break;
            case Player.CurrentPlayer.p4:
                switch (currentPlayer.currentCamera)
                {
                    case Player.CurrentCamera.a:
                        currentNoise = noise4;
                        break;
                    case Player.CurrentCamera.b:
                        currentNoise = noise4b;
                        break;
                    case Player.CurrentCamera.c:
                        currentNoise = noise4c;
                        break;
                    case Player.CurrentCamera.d:
                        currentNoise = noise4d;
                        break;
                    case Player.CurrentCamera.e:
                        currentNoise = noise4e;
                        break;
                }
                break;
        }
    }

    public void StopShake()
    {
        timer = 0.0f;

        if (currentNoise != null)
        {
            currentNoise.m_AmplitudeGain = 0.0f;
            currentNoise.m_FrequencyGain = 0.0f;

            currentNoise = null;
        }
    }
}