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

    //���߿� Enum���� �ٲ�ߵɵ�

    public string unitType;       // ��: Ground, Air, Building, Spell, Projectile
    public string attackType;     // ��: Melee, Ranged, Splash, Pushback, None
    public string attackTarget;   // ��: GroundOnly, AirOnly, Both, BuildingOnly, None

    public float range;           // ���� ��Ÿ�
    public float attackDelay;     // ���� �ֱ� (��ٿ�)
    public int damage;            // ���ݷ�
    public int hp;                // ü��
    public string speed;          // �̵� �ӵ�: Slow, Medium, Fast

    public int spawnCount;        // ��ȯ �� (ex: ���̷��� 3����)

    public string prefabName;     // ����Ƽ ������ �̸�
    public string iconPath;       // ī�� ������ ��� (UI)
    public string description;    // ī�� ����
    public float lifeTime;        // ���� �ð� (�ǹ�/���� �� �Ͻ� ����)
}