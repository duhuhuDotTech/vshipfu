using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;

public class Main : MonoBehaviour
{

    void Start()
    {
        init();
        //Application.targetFrameRate = 60
    }

    public void init()
    {

    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            UnityEngine.Application.Quit();
        }
    }
}
