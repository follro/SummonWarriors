using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    private TextAsset jsonGameDatas;
    public GameDataContainer GameDatas { get; private set; }

    private void Awake()
    {
        PreserveSingleton();

        jsonGameDatas = AssetLoadManager.Instance.LoadAsset<TextAsset>("Data/SummonWarriorsDatas");
        if (jsonGameDatas != null)
            GameDatas = JsonUtility.FromJson<GameDataContainer>(jsonGameDatas.text);
        else
            Debug.Log($"{typeof(GameDataManager).Name}: init Error");
    }



    private void Start()
    {
        //임시 유닛 생성 테스트
        /*foreach (var curCardData in GameDatas.cardDatas)
        {
            Unit.Unit unit = Instantiate(testUnit);
            unit.UnitStatus = new Unit.UnitData(curCardData);
            unit.gameObject.name = curCardData.unitName;
        }*/
    }




}
