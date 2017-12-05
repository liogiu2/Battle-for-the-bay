using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnemyList : MonoBehaviour {

	private AIRootScript _destroyingItem;
	// Use this for initialization

	public void AddDestroyingItem(AIRootScript item){
		_destroyingItem = item;
	}

	public AIRootScript GetDestroyingItem(){
		return _destroyingItem;
	}

	public void RemoveAiRootScript(){
		_destroyingItem = null;
	}

}
