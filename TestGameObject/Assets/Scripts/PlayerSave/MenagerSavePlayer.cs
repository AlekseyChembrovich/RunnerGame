using System.IO;
using UnityEngine;

namespace Assets.Scripts.PlayerSave
{
    public class MenagerSavePlayer
    {
        private PlayerClass player = new PlayerClass();
        private static string PathFile
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return Path.Combine(Application.persistentDataPath, "PlayerSave.json");
#else
                return Path.Combine(Application.dataPath, "PlayerSave.json");
#endif
            }
        }

        private static MenagerSavePlayer singleMenager;

        private MenagerSavePlayer() { }

        public static MenagerSavePlayer GetMenager()
        {
            singleMenager = singleMenager ?? new MenagerSavePlayer();
            return singleMenager;
        }

        public PlayerClass ReadDataPlayer()
        {
            var path = PathFile;
            if (File.Exists(path))
            {
                using (var file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (var streamReader = new StreamReader(file))
                    {
                        player = JsonUtility.FromJson<PlayerClass>(streamReader.ReadToEnd());
                    }
                }
            }
            return player;
        }

        public void WriteDataPlayer()
        {
            var path = PathFile;
            File.WriteAllText(path, JsonUtility.ToJson(player));
        }

        public bool WriteDataPlayer(float comparisonValue, int countCoins)
        {
            if (!(player.CountSecond < comparisonValue)) return false;
            player.CountCoins += countCoins;
            player.CountSecond = comparisonValue;
            WriteDataPlayer();
            return true;
        }

        public void WriteDataPlayer(int countCoins)
        {
            player.CountCoins += countCoins;
            WriteDataPlayer();
        }
    }
}