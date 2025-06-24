using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private TextAsset jsonGameDatas;
    private AllCardData allCardData;
    // Start is called before the first frame update

    private void Awake()
    {
        jsonGameDatas = Resources.Load<TextAsset>("Resources/Data/SummonWarriorsDatas");
        allCardData = JsonUtility.FromJson<AllCardData>(jsonGameDatas.text);

        foreach (var VARIABLE in allCardData.cardDatas)
        {
            print(VARIABLE.name); 
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum unitType
{
    Ground, Air, Building, Spell, End
}
public enum attackType
{
    Melee, Ranged, Splash, Area, End
}
public enum attackTarget
{
    Ground, Air, Building, End
}
public enum speed
{
    Slow, Medium, Fast, End
}

[System.Serializable]
public class AllCardData
{
    public CardData[] cardDatas;
}

[System.Serializable]
public class CardData
{
    public int id;
    public string name;
    public int cost;

    //나중에 Enum으로 바꿔야될듯

    public string unitType;       // 예: Ground, Air, Building, Spell, Projectile
    public string attackType;     // 예: Melee, Ranged, Splash, Pushback, None
    public string attackTarget;   // 예: GroundOnly, AirOnly, Both, BuildingOnly, None

    public float range;           // 공격 사거리
    public float attackDelay;     // 공격 주기 (쿨다운)
    public int damage;            // 공격력
    public int hp;                // 체력
    public string speed;          // 이동 속도: Slow, Medium, Fast

    public int spawnCount;        // 소환 수 (ex: 스켈레톤 3마리)

    public string prefabName;     // 유니티 프리팹 이름
    public string iconPath;       // 카드 아이콘 경로 (UI)
    public string description;    // 카드 설명
    public float lifeTime;        // 지속 시간 (건물/스펠 등 일시 유닛)
}