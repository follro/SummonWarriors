using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private TextAsset jsonGameDatas;
    private GameDataContainer gameDataContainer;
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
}



