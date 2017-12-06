using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnemyList : MonoBehaviour
{

    public AIRootScript DestroyingItem;
    public GameObject DestroyingGameObject;

    public void AddDestroyingItem(AIRootScript item)
    {
        DestroyingItem = item;
        DestroyingGameObject = item.gameObject;
        StartCoroutine(NullObjects());
    }


    private IEnumerator NullObjects()
    {
        yield return new WaitForSeconds(0.1f);
		DestroyingItem = null;
        DestroyingGameObject = null;
    }
}
