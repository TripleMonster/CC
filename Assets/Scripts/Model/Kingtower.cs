using System;
using System.Collections.Generic;

[Serializable]
public class KingTowers
{
	public Dictionary<string, KingTower> kingTowers = new Dictionary<string, KingTower>();
}

[Serializable]
public class KingTower
{
	public int hp;
	public int act;
}
