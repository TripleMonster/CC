using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Newtonsoft.Json;

public class DataManager : Singleton<DataManager>
{
    private Cards cards;
    private KingTowers kingTowers;
    private We weCamp;
    private Enemy enemyCamp;

    public Dictionary<string, Sprite> battleAllCardSprites = new Dictionary<string, Sprite>();

    #region private structure function
    private DataManager() { }
    #endregion


    #region configs
    public void SetCardsByTextAsset(TextAsset textAsset)
    {
        Cards lCards = JsonConvert.DeserializeObject<Cards>(textAsset.text);
        this.cards = lCards;
    }

    public void SetKingTowersByTextAsset(TextAsset textAsset)
    {
        KingTowers lKingTowers = JsonConvert.DeserializeObject<KingTowers>(textAsset.text);
        this.kingTowers = lKingTowers;
    }

    public void SetEnemyCampByTextAsset(TextAsset textAsset)
    {
        Enemy enemy = JsonConvert.DeserializeObject<Enemy>(textAsset.text);
        this.enemyCamp = enemy;
    }

    public void SetWeCampByTextAsset(TextAsset textAsset)
    {
        We we = JsonConvert.DeserializeObject<We>(textAsset.text);
        this.weCamp = we;
    }
    #endregion




    #region atlas
    public void SetCardSpritesByAtlas(SpriteAtlas spriteAtlas)
    {
        foreach (var cardName in enemyCamp.cards)
        {
            if (cardName != null)
            {
                var sprite = spriteAtlas.GetSprite(cardName);
                if (sprite != null)
                    battleAllCardSprites.Add(cardName, sprite);
            }
        }

        foreach (var cardName in weCamp.cards)
        {
            if (cardName != null)
            {
                var sprite = spriteAtlas.GetSprite(cardName);
                if (sprite != null)
                    battleAllCardSprites.Add(cardName, sprite);
            }
        }
    }

    #endregion
}
