using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    public Queue<Sprite> spritesQueue = new Queue<Sprite>();

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
                {
                    battleAllCardSprites.Add(cardName, sprite);
                    spritesQueue.Enqueue(sprite);
                }
            }
        }

        //foreach (var cardName in weCamp.cards)
        //{
        //    if (cardName != null)
        //    {
        //        var sprite = spriteAtlas.GetSprite(cardName);
        //        if (sprite != null)
        //            battleAllCardSprites.Add(cardName, sprite);
        //    }
        //}
    }


    public Sprite getARandomCard()
    {
        Sprite sprite = spritesQueue.Dequeue();
        Debug.Log("从牌池随机一张卡牌 : " + sprite.name);
        spritesQueue.Enqueue(sprite);
        return sprite;
    }
    #endregion
}
