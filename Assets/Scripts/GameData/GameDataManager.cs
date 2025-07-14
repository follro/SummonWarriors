using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AssetLoadManager;

public class GameDataManager : Singleton<GameDataManager>
{
    //�� const �����͵� ���߿� ���������Ʈ�� �Űܾߵ�
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
            Debug.LogError($"{typeof(GameDataManager).Name}: JSON ������ TextAsset �ε� ����");
            return false;
        }

        if (gameData != null)
        {
            //ī�嵥���� �ʸ� �޾ƿ��� �߰��� �ʿ��� �����͵� foreach�� �߰�
            foreach (var cardData in gameData.cardDatas)
            {
                if (!CardDatasById.ContainsKey(cardData.id))
                    CardDatasById.Add(cardData.id, cardData);
                else
                    Debug.LogWarning($"{typeof(GameDataManager).Name}: �ߺ��� Card ID�� �߰ߵǾ����ϴ�: {cardData.id}. �ش� ī��� ���õ˴ϴ�.");
            }
        }
        else
        {
            Debug.LogError($"{typeof(GameDataManager).Name}: {typeof(GameDataContainer).Name}�� ����ֽ��ϴ�.");
            return false;
        }

        return true;
    }


}
