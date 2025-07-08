using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private TextAsset jsonGameDatas;
    private GameDataContainer gameDataContainer;
    [SerializeField]
    private Unit.Unit testUnit;

    private void Awake()
    {
        PreserveSingleton();

        jsonGameDatas = Resources.Load<TextAsset>("Data/SummonWarriorsDatas");
        if (jsonGameDatas != null)
        {
            gameDataContainer = JsonUtility.FromJson<GameDataContainer>(jsonGameDatas.text);

            foreach (var curCardData in gameDataContainer.cardDatas)
            {
                Unit.UnitData unitData = new Unit.UnitData(curCardData);
                print(unitData.AttackAbleTypes);
            }
        }
        else
            Debug.Log("init Error");
    }

    private void Start()
    {
        //�ӽ� ���� ���� �׽�Ʈ
        foreach (var curCardData in gameDataContainer.cardDatas)
        {
            Unit.Unit unit = Instantiate(testUnit);
            unit.UnitStatus = new Unit.UnitData(curCardData);
            unit.gameObject.name = curCardData.unitName;
        }

    }


}
