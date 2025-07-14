using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    
    [System.Serializable]
    public class CardData
    {
        public int id;
        public string unitName;
        public int cost;

        public string unitType;       // ��: Ground, Air, Building, Spell, Projectile
        public string attackType;     // ��: Melee, Ranged, Splash, Pushback, None
        public string[] attackAbleTypes;   // ��: GroundOnly, AirOnly, Both, BuildingOnly, None
        public float range;           // ���� ��Ÿ�
        public float attackDelay;     // ���� �ֱ� (��ٿ�)
        public int damage;            // ���ݷ�
        public int hp;                // ü��
        public string speedType;          // �̵� �ӵ�: Slow, Medium, Fast

        public float spawnDelay;        //���� ��� �ð�
        public int spawnCount;        // ��ȯ �� (ex: ���̷��� 3����)
            
        public string prefabName;     // ����Ƽ ������ �̸�
        public string iconPath;       // ī�� ������ ��� (UI)
        public string description;    // ī�� ����
        public float lifeTime;        // ���� �ð� (�ǹ�/���� �� �Ͻ� ����)
    }

    [System.Serializable]
    public class GameDataContainer
    {
        public List<CardData> cardDatas;
    }
}
