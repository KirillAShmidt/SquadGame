using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInterface : MonoBehaviour
{
    public static GInterface Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Instance = this;
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
