using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : MonoBehaviour
{
    private static SSDirector instance;
    public ISceneController CurrentScenceController { get; set; }
    public static SSDirector GetInstance()
    {
        if (instance == null)
        {
            instance = new SSDirector();
        }
        return instance;
    }
}
