﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class CCActionManager : SSActionManager, ISSActionCallback
{
    public SSActionEventType Complete = SSActionEventType.Completed;
    int count = 0;

    public void MoveDisk(DiskData disk)
    {
        ++count;
        Complete = SSActionEventType.Started;
        CCMoveToAction diskAction = CCMoveToAction.GetAction(disk.Speed);
        this.addAction(disk.gameObject, diskAction, this);
    }

    public void SSActionCallback(SSAction source)
    {
        --count;
        Complete = SSActionEventType.Completed;
        source.gameObject.SetActive(false);
    }
}
