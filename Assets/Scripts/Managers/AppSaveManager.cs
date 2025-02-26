using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace KidsTest
{
    public static class AppSaveManager
    {
        private static string SaveFilePath => Application.persistentDataPath + "/save.data";

        public static void SaveCharacterData(Dictionary<CustomCharacterPartType, int> partsIndex)
        {
            try
            {
                CharacterDataSerialized charData = new CharacterDataSerialized()
                {
                    AccessoryIndex = partsIndex[CustomCharacterPartType.Accessory],
                    GlassesIndex = partsIndex[CustomCharacterPartType.Glasses],
                    HairIndex = partsIndex[CustomCharacterPartType.Hair],
                    HatIndex = partsIndex[CustomCharacterPartType.Hat],
                    PantsIndex = partsIndex[CustomCharacterPartType.Pants],
                    OuterIndex = partsIndex[CustomCharacterPartType.Outer],
                    ShoesIndex = partsIndex[CustomCharacterPartType.Shoes]
                };

                FileStream dataStream = new FileStream(SaveFilePath, FileMode.Create);

                BinaryFormatter converter = new BinaryFormatter();
                converter.Serialize(dataStream, charData);
                dataStream.Close();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static CharacterDataSerialized LoadCharacterData()
        {
            try
            {
                if (File.Exists(SaveFilePath))
                {
                    FileStream dataStream = new FileStream(SaveFilePath, FileMode.Open);

                    BinaryFormatter converter = new BinaryFormatter();
                    CharacterDataSerialized charData = converter.Deserialize(dataStream) as CharacterDataSerialized;
                    dataStream.Close();

                    return charData;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return null;
        }
    }

    [Serializable]
    public class CharacterDataSerialized
    {
        public int BodyIndex;
        public int AccessoryIndex;
        public int GlassesIndex;
        public int HairIndex;
        public int HatIndex;
        public int PantsIndex;
        public int OuterIndex;
        public int ShoesIndex;
    }
}
