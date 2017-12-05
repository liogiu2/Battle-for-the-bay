using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnemyList : MonoBehaviour
{

    public AIRootScript DestroyingItem;

    public void AddDestroyingItem(AIRootScript item)
    {
        DestroyingItem = item;
    }


    private IEnumerator Fire(GameObject Target)
    {
        yield return new WaitForSeconds(0.1f);
		DestroyingItem = null;
    }


}
