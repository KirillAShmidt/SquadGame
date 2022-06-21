using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour
{
    public static GameCameraController Instance;

    private void Start()
    {
        Instance = this;
    }
}
