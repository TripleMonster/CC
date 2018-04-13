using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonTest : MonoBehaviour {

    int testInt;
	void Start () {
        /*
        TextAsset jsonText = Resources.Load("Json/ake") as TextAsset;
        //Hero jsonObj = JsonUtility.FromJson<Hero>(jsonText.text);
        Hero jsonObj = JsonConvert.DeserializeObject<Hero>(jsonText.text);

        Debug.Log("hero.name=" + jsonObj.name);
        Debug.Log("hero.price=" + jsonObj.price);

        foreach (var skill in jsonObj.skills)
        {
            Debug.Log("skill.name=" + skill.name + "; skill.cool=" + skill.cool);
        }

        foreach (var attribute in jsonObj.attributes)
        {
			Debug.Log("attribute." + attribute.Key + "=" + attribute.Value);
        }
        */

        //TextAsset csvText = Resources.Load<TextAsset>("CSV/ChickenTeam");
        //Debug.Log(csvText.text);
        testInt = 8;
        Debug.Log("Start----" + testInt.ToString());
    }

	private void Awake()
	{
        Debug.Log("awake");
	}

	private void OnEnable()
	{
        Debug.Log("onEnable" + testInt.ToString());
	}

    bool isupdate = false;
	private void Update()
	{
        if (!isupdate){
            isupdate = true;
            Debug.Log("update");    
        }
	}

}

[Serializable]
public class Team {
    public int Id;
    public string Name;
    public string Desc;
}


[Serializable]
public class Hero {
    public string name;
    public int price;
    public List<Skill> skills;
    public Dictionary<string, string> attributes;
}

[Serializable]
public class Skill {
    public string name;
    public int cool;
}
