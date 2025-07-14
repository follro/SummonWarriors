using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AssetLoadManager;

public class GameDataManager : Singleton<GameDataManager>
{
    //이 const 데이터들 나중에 스프레드시트로 옮겨야됨
    private const int DeckSize = 4;
    private const int PlayerNum = 2;
    private const int CardTypeNum = 10000;
    private const int CardIdStartNum = 100000000;


    private string LocalJsonDataPath = "Data/SummonWarriorsDatas";
    private TextAsset jsonGameDatas;
    public CardData[,] PlayersDeckData { get; private set; }
    public Dictionary<int, CardData> CardDatasById { get; private set; }


    private void Awake()
    {
        PreserveSingleton();
        CardDatasById = new Dictionary<int, CardData>();
        PlayersDeckData = new CardData[PlayerNum, DeckSize];  
    }


    private void Start()
    {
        LocalDataLoad();
        TempDeckSetting();
    }
    #region TempFuncs

    private void TempDeckSetting()
    {
        for(int i = 0; i < PlayerNum; i++)
        {
            for (int j = 1; j <= DeckSize; j++)
            {
                CardDatasById.TryGetValue(i * CardTypeNum + CardIdStartNum + j, out PlayersDeckData[i, j -1]);
            }
        }
    }

    #endregion

    private bool LocalDataLoad()
    {
        GameDataContainer gameData = null;
        jsonGameDatas = AssetLoadManager.Instance.LoadAsset<TextAsset>(AssetLoadMode.Resources, LocalJsonDataPath);
        
        if (jsonGameDatas != null)
            gameData = JsonUtility.FromJson<GameDataContainer>(jsonGameDatas.text);
        else
        {
            Debug.LogError($"{typeof(GameDataManager).Name}: JSON 데이터 TextAsset 로드 실패");
            return false;
        }

        if (gameData != null)
        {
            //카드데이터 쪽만 받아오기 추가로 필요한 데이터들 foreach로 추가
            foreach (var cardData in gameData.cardDatas)
            {
                if (!CardDatasById.ContainsKey(cardData.id))
                    CardDatasById.Add(cardData.id, cardData);
                else
                    Debug.LogWarning($"{typeof(GameDataManager).Name}: 중복된 Card ID가 발견되었습니다: {cardData.id}. 해당 카드는 무시됩니다.");
            }
        }
        else
        {
            Debug.LogError($"{typeof(GameDataManager).Name}: {typeof(GameDataContainer).Name}이 비어있습니다.");
            return false;
        }

        return true;
    }


}
