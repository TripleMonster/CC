using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class TTPoolsManager : MonoSigleton<TTPoolsManager> {
    SpawnPool heroPool;
    SpawnPool bloodBarPool;

	void Start () 
    {
        heroPool = PoolManager.Pools["Hero"];
        bloodBarPool = PoolManager.Pools["BloodBar"];
	}
	
	void Update () 
    {
		
	}

    public void AddPrfabToPool(GameObject prefabObj)
    {
        PrefabPool prefabPool = new PrefabPool(prefabObj.transform);
        prefabPool.preloadAmount = 2;
        prefabPool.limitInstances = true;
        prefabPool.limitFIFO = false;
        prefabPool.limitAmount = 5;
        prefabPool.cullDespawned = true;
        prefabPool.cullAbove = 16;
        prefabPool.cullDelay = 2;
        heroPool._perPrefabPoolOptions.Add(prefabPool);
        heroPool.CreatePrefabPool(prefabPool);
    }

    public Transform GetPrefabFromPool(string prefabName)
    {
        return heroPool.Spawn(prefabName);
    }
    
    public Transform GetBloodBarPrefabFromPool(string prefabName)
    {
        return bloodBarPool.Spawn(prefabName);
    }
}
