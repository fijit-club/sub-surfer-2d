using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using TMPro;

namespace SpaceEscape
{
    public class NativeAPI
    {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        public static extern void sendMessageToMobileApp(string message);
#endif
    }


    [System.Serializable]
    public class Attributes
    {
        public string id;
        public int level;
    }
    
    [System.Serializable]
    public class Assets
    {
        public string id;
        public Attributes[] attributes;

        public Assets(string spaceshipId)
        {
            id = spaceshipId;

        }
    }
    
    [System.Serializable]
    public class Data
    {
        public List<Assets> assets;
        public string saveData;
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public int coins;
        public bool volumeBg = true;
        public bool volumeSfx = true;
        public int highScore = 0;
        public Data data;
        public static PlayerInfo CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<PlayerInfo>(jsonString);
        }
    }



    //[System.Serializable]
    //public class PlayerInfo
    //{
    //    public int coins;
    //    public PlayerData playerData;
    //    public static PlayerInfo CreateFromJSON(string jsonString)
    //    {
    //        return JsonUtility.FromJson<PlayerInfo>(jsonString);
    //    }
    //}

    public class Bridge : MonoBehaviour
    {
        public PlayerInfo thisPlayerInfo;
        private static Bridge instance;
        public int coinsCollected = 0;
        [SerializeField] private GameOver gameOverMenu;
        [SerializeField] private TMP_Text highscoreText;
        [SerializeField] private TMP_Text highscoreText2;
        
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void setScore(int score);

    //      [DllImport("__Internal")]
    // private static extern void registerVisibilityChangeEvent();


        [DllImport("__Internal")]
        private static extern void buyAsset(string assetId);

        [DllImport("__Internal")]
        private static extern void updateCoins(int coinsChange);

        [DllImport("__Internal")]
        private static extern void updateExp(int expChange);
        
        // [DllImport("__Internal")]
        // private static extern void upgradeAsset(string assetID, string attributeID, int level);

        [DllImport("__Internal")]
        private static extern void load();

        [DllImport("__Internal")]
        private static extern void restart();

        [DllImport("__Internal")]
        private static extern void vibrate(bool isLong);
#endif

        public static Bridge GetInstance()
        {
            return instance;
        }

        private void Start()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
        
#endif
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            Debug.Log("Loaded");
#if UNITY_WEBGL && !UNITY_EDITOR
            load();
#endif
               
                //SendInitSendInitialData("{ \"coins\": 6969, \"data\":{\"cars\": [{\"id\": \"default-car\"}]}   }");
            }
            else
                Destroy(this);


        }


        public void AddExp(int exp)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            updateExp(exp);
#endif
        }

        public void GameLoaded()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            load();
#endif
        }

        public void ButtonPressed()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
                {
                    jc.CallStatic("sendMessageToMobileApp", "The button has been tapped!");
                }
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IOS && !UNITY_EDITOR
                NativeAPI.sendMessageToMobileApp("The button has been tapped!");
#endif
            }
        }
        
        public void VibrateBridge(bool isLong)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
    if(thisPlayerInfo.volumeBg)
      vibrate(isLong);
#endif
#if UNITY_EDITOR
            if (thisPlayerInfo.volumeBg)
                Debug.Log("vibrating device " + isLong);
#endif
        }

        public void SendScore(int score)
        {

            Debug.Log(coinsCollected + "sent coin");
#if UNITY_WEBGL && !UNITY_EDITOR
            //updateCoins(coinsCollected);
#endif
#if UNITY_WEBGL && !UNITY_EDITOR

            setScore(score);
#elif UNITY_EDITOR
            Debug.Log("sendingscore" + score);
#endif
        }

        public void Mute()
        {
            //SoundManager.Mute();
            AudioListener.volume = 0;
        }

        public void Unmute()
        {
            //SoundManager.Unmute();
            AudioListener.volume = 1;
        }

        public void Replay()
        {
            coinsCollected = 0; // REPLAY GOES HERE
            highscoreText.text = thisPlayerInfo.highScore.ToString();
            highscoreText2.text = thisPlayerInfo.highScore.ToString();
            gameOverMenu.Restart();
        }

        public void SendInitialData(string json)
        {
            thisPlayerInfo = PlayerInfo.CreateFromJSON(json);
            Debug.Log(json);
            highscoreText.text = thisPlayerInfo.highScore.ToString();
            highscoreText2.text = thisPlayerInfo.highScore.ToString();

            if (thisPlayerInfo.volumeSfx)
            {
                Silence("false");
            }
            else
            {
                Silence("true");

            }
            
            //Replay();
            //Events.CoinsCountChanged.Call();
        }

        public void AddCoin()
        {
            thisPlayerInfo.coins++;
        }

        public void UpdateCoins(int value)
        {
            thisPlayerInfo.coins += value;
            coinsCollected += value;
            if (value > 0)
            {
            Debug.Log(value);
#if UNITY_WEBGL && !UNITY_EDITOR
            updateCoins(coinsCollected);
#endif
            }
        }

        public void CollectCoins(int value)
        {
            thisPlayerInfo.coins += value;
            coinsCollected += value;
            //Debug.Log(value);

        }

//         public void SetShootPower(int value)
//         {
//             string gameState = JsonUtility.ToJson(thisPlayerInfo.data);
//             Debug.Log(gameState);
//
// #if UNITY_WEBGL && !UNITY_EDITOR
//            upgradeAsset(thisPlayerInfo.data.cannons[0].id, thisPlayerInfo.data.cannons[0].attributes[1].id, thisPlayerInfo.data.cannons[0].attributes[1].level);
// #endif
//         }
//         public void SetShootSpeed(int value)
//         {
//
// #if UNITY_WEBGL && !UNITY_EDITOR
//             upgradeAsset(thisPlayerInfo.data.cannons[0].id, thisPlayerInfo.data.cannons[0].attributes[0].id, thisPlayerInfo.data.cannons[0].attributes[0].level);
// #endif
//         }

        public void BuySpaceship(string spaceshipID)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
                    buyAsset(spaceshipID);
#endif
            AddSpaceship(spaceshipID);
        }

        public void AddSpaceship(string spaceshipID)
        {
            Assets addedAssets = new Assets(spaceshipID);
            addedAssets.id = spaceshipID;
        
        
        
            Debug.Log("added new spaceship " + addedAssets.id);
        
            thisPlayerInfo.data.assets.Add(addedAssets);
        }

        [ContextMenu("Do Something")]
        public void SendTextData()
        {
            //SendInitialData("{\"coins\": 123,\"playerData\": {\"shootPower\":25,\"shootSpeed\":20}}");
            //SendInitialData("{\"coins\":3400,\"data\":{\"cannons\":[{\"id\":\"bvb-cannon-1\",\"attributes\":[{\"bvb-cannon-1-speed\":0,\"bvb-cannon-1-power\":0}]}]}}");
            //SendInitialData("{\"coins\":34,\"data\":{\"cars\":[{\"id\":\"bvb-cannon-1\",\"attributes\":[{\"id\":\"bvb-cannon-1-speed\",\"level\":91},{\"id\":\"bvb-cannon-1-power\",\"level\":92}]},{\"id\":\"bvb-cannon-2\",\"attributes\":[{\"id\":\"bvb-cannon-2-speed\",\"level\":3},{\"id\":\"bvb-cannon-2-power\",\"level\":2}]}]}}");
            //SendInitialData("{\"coins\": 3000,\"data\": null}");
            //Debug.Log(JsonUtility.ToJson( thisPlayerInfo.data));
            //Debug.Log( thisPlayerInfo.data);
            SendInitialData("{\"coins\":384696,\"volumeBg\":true,\"volumeSfx\":true,\"highScore\":949,\"data\":{\"assets\":[{\"id\":\"space-escape-slow-time-ship\",\"attributes\":[]},{\"id\":\"space-escape-cancel-explosion-ship\",\"attributes\":[]}], \"saveData\":" +
                            "\"TEST\"" +
                            "}}");
        }
        [ContextMenu("Do Something2")]
        public void SendTextData2()
        {
            //SetShootSpeed(50);

        }

        public int GetCoins()
        {
            return thisPlayerInfo.coins;
        }


        public void Silence(string silence)
        {
            if (silence == "true")
                AudioListener.pause = true;

            if (silence == "false")
                AudioListener.pause = false;
            // Or / And
            //AudioListener.volume = silence ? 0 : 1;

            System.Console.WriteLine("called silence " + silence);

        }


    }
}
