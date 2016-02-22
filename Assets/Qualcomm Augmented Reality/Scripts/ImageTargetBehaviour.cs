/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Vuforia
{
    /// <summary>
    /// This class serves both as an augmentation definition for an ImageTarget in the editor
    /// as well as a tracked image target result at runtime
    /// </summary>
    public class ImageTargetBehaviour : ImageTargetAbstractBehaviour
    {
      void Start()
      {
        Debug.Log("Woohoo");
      }
    }
}
