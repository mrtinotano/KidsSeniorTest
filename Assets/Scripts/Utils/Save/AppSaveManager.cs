using KidsTest.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace KidsTest
{
    public class AppSaveManager : PersistentSingleton<AppSaveManager>
    {
        private string SaveFilePath => Application.persistentDataPath + "/save.data";

        public void SaveCharacterData(Dictionary<CustomCharacterParts, int> partsIndex)
        {
            try
            {
                CharacterDataSerialized charData = new CharacterDataSerialized()
                {
                    BodyIndex = partsIndex[CustomCharacterParts.Body],
                    AccessoryIndex = partsIndex[CustomCharacterParts.Accessory],
                    GlassesIndex = partsIndex[CustomCharacterParts.Glasses],
                    HairIndex = partsIndex[CustomCharacterParts.Hair],
                    HatIndex = partsIndex[CustomCharacterParts.Hat],
                    PantsIndex = partsIndex[CustomCharacterParts.Pants],
                    OuterIndex = partsIndex[CustomCharacterParts.Outer],
                    ShoesIndex = partsIndex[CustomCharacterParts.Shoes]
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

        public CharacterDataSerialized LoadCharacterData()
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
