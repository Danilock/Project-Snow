using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField, Range(1, 120)] private int _frameRate;

    private void Update()
    {
        Application.targetFrameRate = _frameRate;
    }
}
