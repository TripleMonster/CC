using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEventUtils;
using UnityEngine.U2D;
using Newtonsoft.Json;

public class Loading : MonoBehaviour 
{
    enum ConfigTag { CARD = 10, KINGTOWER = 11, ENEMY = 12, WE = 13 }
    enum AtlasTag { CARD = 20 , UI = 21 }

    [HideInInspector]public UEvent_f loadingCompleted = new UEvent_f();

	void Start () 
    {
		
	}
	
	void Update () 
    {
		
	}

    public void LoadingConfig()
    {
        ResourcesManager.Instance.LoadAsset<TextAsset>("Configs/cardconfig", LoadingConfigCompleted, (result) => {
            Debug.Log(result);
        });
        ResourcesManager.Instance.LoadAsset<TextAsset>("Configs/kingTower", LoadingConfigCompleted, (result) => {
            Debug.Log(result);
        });
        ResourcesManager.Instance.LoadAsset<TextAsset>("Configs/enemyconfig", LoadingConfigCompleted, (result) => {
            Debug.Log(result);
        });
        ResourcesManager.Instance.LoadAsset<TextAsset>("Configs/weconfig", LoadingConfigCompleted, (result) => {
            Debug.Log(result);
        });
    }

    public void LoadingAtlas()
    {
        ResourcesManager.Instance.LoadAsset<SpriteAtlas>("Atlas/CardsAtlas", LoadingAtlasCompleted, (result) => {
            Debug.Log(result);
        });

        ResourcesManager.Instance.LoadAsset<SpriteAtlas>("Atlas/BattleUIAtlas", LoadingAtlasCompleted, (result) => {
            Debug.Log(result);
        });
    }

    public void LoadingPrefab()
    {
        foreach (var item in DataManager.Instance.battleAllCardSprites)
        {
            string cardname = item.Key;
            Debug.Log("card name : " + cardname);
            ResourcesManager.Instance.LoadAsset<GameObject>("Prefabs/Hero/" + cardname, LoadingPrefabCompleted, (result) => {
                Debug.Log(result);
            });
        }
    }

    public void LoadingAudio()
    {
        
    }

    void LoadingConfigCompleted(TextAsset textAsset)
    {
        Debug.Log("text asset name : " + textAsset.name);
            switch(textAsset.name)
        {
            case "cardconfig":
                DataManager.Instance.SetCardsByTextAsset(textAsset);
                loadingCompleted.Invoke(0.05f);
                break;
            case "kingTower":
                DataManager.Instance.SetKingTowersByTextAsset(textAsset);
                loadingCompleted.Invoke(0.05f);
                break;
            case "enemyconfig":
                DataManager.Instance.SetEnemyCampByTextAsset(textAsset);
                loadingCompleted.Invoke(0.05f);
                break;
            case "weconfig":
                DataManager.Instance.SetWeCampByTextAsset(textAsset);
                loadingCompleted.Invoke(0.05f);
                break;
        }
    }

    void LoadingAtlasCompleted(SpriteAtlas spriteAtlas)
    {
        Debug.Log("sprite atlas name" + spriteAtlas.name);
        switch(spriteAtlas.name)
        {
            case "CardsAtlas":
                DataManager.Instance.SetCardSpritesByAtlas(spriteAtlas);
                loadingCompleted.Invoke(0.1f);
                break;
            case "BattleUIAtlas":
                loadingCompleted.Invoke(0.1f);
                break;
        }
    }

    void LoadingPrefabCompleted(GameObject prefabObj)
    {
        Debug.Log("prefab name :" + prefabObj.name);
        loadingCompleted.Invoke(0.025f);
    }
}
