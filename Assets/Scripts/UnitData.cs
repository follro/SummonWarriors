using GameData;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Unit
{
    public class UnitData
    {
        public int Id { get; private set; }
        public string UnitName { get; private set; }
        public int Cost { get; private set; }

        public UnitType UnitType { get; private set; }
        public AttackType AttackType { get; private set; }
        public AttackAbleType AttackAbleTypes { get; private set; } 

        public float Range { get; private set; }
        public float AttackDelay { get; private set; }
        public int Damage { get; private set; }

        public SpeedType SpeedType { get; private set; }
        public float SpawnDelay { get; private set; }
        public int SpawnCount { get; private set; }

        public string IconPath { get; private set; }
        public float LifeTime { get; private set; }

        public int MaxHP { get; private set; }
        public int CurrentHP { get; set; }

        public UnitData(CardData cardData)
        {
            Id = cardData.id;
            UnitName = cardData.unitName;
            Cost = cardData.cost;

            UnitType = EnumUtil.ParseEnum<UnitType>(cardData.unitType);
            AttackType = EnumUtil.ParseEnum<AttackType>(cardData.attackType);
            AttackAbleTypes = EnumUtil.ParseEnum<AttackAbleType>(cardData.attackAbleTypes);

            Range = cardData.range;
            AttackDelay = cardData.attackDelay;
            Damage = cardData.damage;

            MaxHP = cardData.hp;
            CurrentHP = MaxHP;

            SpeedType = EnumUtil.ParseEnum<SpeedType>(cardData.speedType);
            SpawnDelay = cardData.spawnDelay;
            SpawnCount = cardData.spawnCount;

            IconPath = cardData.iconPath;
            LifeTime = cardData.lifeTime;
        }
    }
}
