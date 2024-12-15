/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallController : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.Init();
    }
}
