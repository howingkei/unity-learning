﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCActionManager : SSActionManager, ISSActionCallback
{
    public CCFlyAction flyAction;
    //控制器
    public FirstController controller;

    protected new void Start()
    {
        controller = (FirstController)SSDirector.GetInstance().CurrentScenceController;
        controller.actionManager = this;
    }

    public void Fly(GameObject disk, float speed, Vector3 direction)
    {
        flyAction = CCFlyAction.GetSSAction(direction, speed);
        RunAction(disk, flyAction, this);
    }

    //回调函数
    public void SSActionEvent(SSAction source,
    SSActionEventType events = SSActionEventType.Competeted,
    int intParam = 0,
    string strParam = null,
    Object objectParam = null)
    {
        //飞碟结束飞行后进行回收
        controller.diskFactory.FreeDisk(source.gameobject);
    }
}
