using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EActiveEnvironment
{
    STARTUP,
    STADIUM,
}

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance;
    public GameObject startup;
    public GameObject stadium;

    private void Awake()
    {
        Instance = this;
    }

    public void SetActiveEnvironment(EActiveEnvironment environment)
    {
        if (environment == EActiveEnvironment.STADIUM)
        {
            startup.SetActive(false);
            stadium.SetActive(true);
        }
        else
        {
            startup.SetActive(true);
            stadium.SetActive(false);
        }
    }
}
