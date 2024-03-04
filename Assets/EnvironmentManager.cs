using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveEnvironment
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

    public void SetActiveEnvironment(ActiveEnvironment environment)
    {
        if (environment == ActiveEnvironment.STADIUM)
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
