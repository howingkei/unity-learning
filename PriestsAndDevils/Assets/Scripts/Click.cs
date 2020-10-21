using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Models;

public class Click : MonoBehaviour
{
	IUserAction action;
	ChaCon characterController;

	public void setController(ChaCon characterCtrl)
	{
		characterController = characterCtrl;
	}

	void Start()
	{
		action = SSDirector.GetInstance().CurrentSceneController as IUserAction;
	}

	void OnMouseDown()
	{
		if (gameObject.name == "boat")
		{
			action.ClickBoat();
		}
		else
		{
			action.ClickCha(characterController);
		}
	}
}

