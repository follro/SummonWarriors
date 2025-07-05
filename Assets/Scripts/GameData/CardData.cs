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

        public string unitType;       // 예: Ground, Air, Building, Spell, Projectile
        public string attackType;     // 예: Melee, Ranged, Splash, Pushback, None
        public string[] attackAbleTypes;   // 예: GroundOnly, AirOnly, Both, BuildingOnly, None
        public float range;           // 공격 사거리
        public float attackDelay;     // 공격 주기 (쿨다운)
        public int damage;            // 공격력
        public int hp;                // 체력
        public string speedType;          // 이동 속도: Slow, Medium, Fast

        public float spawnDelay;        //스폰 대기 시간
        public int spawnCount;        // 소환 수 (ex: 스켈레톤 3마리)
            
        public string prefabName;     // 유니티 프리팹 이름
        public string iconPath;       // 카드 아이콘 경로 (UI)
        public string description;    // 카드 설명
        public float lifeTime;        // 지속 시간 (건물/스펠 등 일시 유닛)
    }

    [System.Serializable]
    public class GameDataContainer
    {
        public List<CardData> cardDatas;
    }
}
