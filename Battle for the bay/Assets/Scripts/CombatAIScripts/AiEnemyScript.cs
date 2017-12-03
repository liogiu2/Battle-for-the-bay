using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemyScript : AIRootScript {

	public void ActivateTarget(){
		transform.Find("Target").gameObject.SetActive(true);
	}

	protected override void OnUpdate(){
		if(enemies.Count == 1){
			TargetEnemy = enemies[0].gameObject;
		}
	}
}
