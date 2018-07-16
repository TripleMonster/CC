using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.U2D;
using Newtonsoft.Json;
using UnityEngine.Assertions;
using UnityEventUtils;

public class DataManager : Singleton<DataManager>
{
    private Cards cards;
    private KingTowers kingTowers;
    private We weCamp;
    private Enemy enemyCamp;

    public Dictionary<string, Sprite> battleAllCardSprites = new Dictionary<string, Sprite>();
    public Queue<Sprite> spritesQueue = new Queue<Sprite>();

    private UserProfile userProfile;

    #region private structure function
    private DataManager() { }
    #endregion


    #region userprofile
    public UEvent_i userProfileEvent = new UEvent_i();
    public void SetUserProfile()
    {
        if (userProfile == null)
        {
            Debug.Log("创建userprofile");
            userProfile = new UserProfile();
            userProfile.userName = "三兽";
            userProfile.userClanName = "北京一醉一陶然";
            userProfile.userLevel = 12;
            userProfile.userExperience = 39308;
            userProfile.userGold = 8240;
            userProfile.userGems = 1024;
            userProfile.userTrophy = 4217;
        }
        else
        {   
            if (userProfile.userLevel != 12)
                userProfileEvent.Invoke(1);
        }
    }

    public UserProfile GetUserProfile()
    {
        return userProfile;
    }

    public void ChangeGoldCount()
    {
        userProfile.userGold += 100;
        userProfileEvent.Invoke(2);
    }

    public void ChangeGemsCount()
    {
        userProfile.userGems += 100;
        userProfileEvent.Invoke(3);
    }

    public void ChangeTrophyCount()
    {
        userProfile.userTrophy += 30;
        userProfileEvent.Invoke(4);
    }
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

    public int GetCardCostByCardName(string cardName)
    {
        Card card = cards.cards[cardName];
        if (card != null)
            return card.cost;
        return 0;
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
