using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //데이터 매니저에게 현재 플레이어가 가진 덱 데이터를 불러들인다(현재는 임시로 모든 데이터를 불러들임)
            List<CardData> cardDatas = GameDataManager.Instance.GameDatas.cardDatas;
            foreach (CardData cardData in cardDatas)
            {
                Unit unit = AssetLoadManager.Instance.LoadAsset<Unit>($"{UnitPath}/{cardData.prefabName}").GetComponent<Unit>();
                ObjectPool<Unit> unitPool = new ObjectPool<Unit>(unit, this.transform, TempObejctPoolSize);
                unitPoolsById.Add(cardData.id, unitPool);
            }
            foreach (CardData cardData in GameDataManager.Instance.GameDatas.cardDatas)
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
            Unit unit = AssetLoadManager.Instance.LoadAsset<Unit>(path);
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
