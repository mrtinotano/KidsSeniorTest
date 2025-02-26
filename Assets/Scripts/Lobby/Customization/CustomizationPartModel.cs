namespace KidsTest
{
    public class CustomizationPartModel
    {
        public CustomCharacterPartType CharacterPartType {  get; private set; }
        public int CurrentMeshIndex { get; private set; }

        public void SetPartType(CustomCharacterPartType characterPartType)
        {
            CharacterPartType = characterPartType;
        }

        public void SetCurrentMeshIndex(int index)
        {
            CurrentMeshIndex = index;
        }
    }
}
