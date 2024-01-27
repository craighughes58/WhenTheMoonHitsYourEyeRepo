/*****************************************************************************
// File Name : AudioEventListener.cs
// Author : Zane Brown
// Creation Date : November 9, 2023
//
// Brief Description : This script does nothing by itself. Must be called by
//                     a UnityEvent on another script to play a sound
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventListener : AudioHelper
{
    /// <summary>
    /// Play the sound attatched to this script
    /// </summary>
    public void PlaySound()
    {
        AudioManager.Instance.PlayClip2D(clipToPlay);
    }
}
