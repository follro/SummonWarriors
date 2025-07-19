using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AssetLoadManager;

namespace Unit.Spawning
{
    public class UnitFactory : Factory<Unit>
    {
        private const string UnitPath = "Prefabs/Units";
        private const int TempObejctPoolSize = 10; 
        //id�� ���� ���ֵ� �� Ǯ�� ����� ���°� �°���?
        private Dictionary<int, ObjectPool<Unit>> unitPoolsById;
        
        private void Awake()
        {
            unitPoolsById = new Dictionary<int, ObjectPool<Unit>>();
        }


        private void Start()
        {
            //�ӽ÷� �� �÷��̾� ���� ��� �����س���
            foreach (CardData cardData in GameDataManager.Instance.PlayersDeckData)
                AddUnitPools(cardData); 
        }

        public void AddUnitPools(CardData cardData)
        {
            if (unitPoolsById.ContainsKey(cardData.id))
            {
                Debug.LogWarning($"{typeof(UnitFactory).Name}: {cardData.unitName}�� Pool�� �̹� �����մϴ�.");
                return;
            }

            string path = $"{UnitPath}/{cardData.prefabName}";
            Unit unit = AssetLoadManager.Instance.LoadAsset<Unit>(AssetLoadMode.Resources, path).GetComponent<Unit>();
            unit.UnitStatus = new UnitData(cardData);

            unitPoolsById.Add(unit.UnitStatus.Id, new ObjectPool<Unit>(unit, this.transform, TempObejctPoolSize)); // �ӽ� ������Ʈ Ǯ
        }
        
        public override Unit GetProduct(Vector3 pos,int id = 0)
        {
            //Unit�� �����͸� �ʱ�ȭ �����ִ°� �ʿ���
            Unit unit = unitPoolsById[id].GetFromPool();

            unit.transform.position = pos;

            return unit;
        }

        public override void ReleaseProduct(Unit product, int id = 0)
        {
            //������ �����͸� �ʱ�ȭ ��Ű�°� �ʿ���
            unitPoolsById[id].ReturnToPool(product); 
        }

     
    }
}
