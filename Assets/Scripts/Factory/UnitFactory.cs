using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AssetLoadManager;

namespace Unit.Spawning
{
    public class UnitFactory : Factory
    {
        private const string UnitPath = "Prefabs/Unit";
        private const int TempObejctPoolSize = 10; 
        //id에 따른 유닛들 별 풀을 만들어 놓는게 맞겠지?
        private Dictionary<int, ObjectPool<Unit>> unitPoolsById;
        
        private void Awake()
        {
            unitPoolsById = new Dictionary<int, ObjectPool<Unit>>();
        }


        private void Start()
        {
            //임시로 양 플레이어 덱에 든거 생성해놓기
            foreach (CardData cardData in GameDataManager.Instance.PlayersDeckData)
            {
                Unit unit = AssetLoadManager.Instance.LoadAsset<Unit>(AssetLoadMode.Resources, $"{UnitPath}/{cardData.prefabName}").GetComponent<Unit>();
                ObjectPool<Unit> unitPool = new ObjectPool<Unit>(unit, this.transform, TempObejctPoolSize);
                unitPoolsById.Add(cardData.id, unitPool);
            }

            foreach (CardData cardData in GameDataManager.Instance.PlayersDeckData)
                AddUnitPools(cardData); 
        }

        public void AddUnitPools(CardData cardData)
        {
            if (unitPoolsById.ContainsKey(cardData.id))
            {
                Debug.LogWarning($"{typeof(UnitFactory).Name}: {this.GetType().Name}의 Pool은 이미 존재합니다.");
                return;
            }
            string path = $"Units/{cardData.prefabName}";
            Unit unit = AssetLoadManager.Instance.LoadAsset<Unit>(AssetLoadMode.Resources, path);
            unit.UnitStatus = new UnitData(cardData);

            unitPoolsById.Add(unit.UnitStatus.Id, new ObjectPool<Unit>(unit, this.transform, TempObejctPoolSize)); // 임시 오브젝트 풀
        }
        
        public override IProduct GetProduct(Vector3 pos, int id = 0)
        {
            Unit unit = unitPoolsById[id].GetFromPool();

            unit.transform.position = pos;

            return unit;
        }

        public void SpawnUnit()
        {
            
        }
    }
}
