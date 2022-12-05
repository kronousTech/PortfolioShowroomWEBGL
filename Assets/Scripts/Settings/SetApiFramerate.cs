using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetApiFramerate : MonoBehaviour
{
    [SerializeField] private int _framerate;

    private void Awake()
    {
        Application.targetFrameRate = _framerate;
    }
}
