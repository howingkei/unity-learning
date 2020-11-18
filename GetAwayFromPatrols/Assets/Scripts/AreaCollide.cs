using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollide : MonoBehaviour
{
    public int sign = 0;
    FirstSceneController sceneController;
    private void Start() 
    { 
        sceneController = SSDirector.GetInstance().CurrentScenceController as FirstSceneController; 
    }
    void OnTriggerEnter(Collider collider) 
    { 
        if (collider.gameObject.name == "Player(Clone)") sceneController.wall_sign = sign; 
    }
}
