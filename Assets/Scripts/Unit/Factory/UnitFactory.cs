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
        //id�� ���� ���ֵ� �� Ǯ�� ����� ���°� �°���?
        private Dictionary<int, ObjectPool<Unit>> unitPoolsById;
        
        private void Awake()
        {
            unitPoolsById = new Dictionary<int, ObjectPool<Unit>>();
        }


        private void Start()
        {
            //������ �Ŵ������� ���� �÷��̾ ���� �� �����͸� �ҷ����δ�(����� �ӽ÷� ��� �����͸� �ҷ�����)
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
                Debug.LogWarning($"{typeof(UnitFactory).Name}: {this.GetType().Name}�� Pool�� �̹� �����մϴ�.");
                return;
            }
            string path = $"Units/{cardData.prefabName}";
            Unit unit = AssetLoadManager.Instance.LoadAsset<Unit>(path);
            unit.UnitStatus = new UnitData(cardData);

            unitPoolsById.Add(unit.UnitStatus.Id, new ObjectPool<Unit>(unit, this.transform, TempObejctPoolSize)); // �ӽ� ������Ʈ Ǯ
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
