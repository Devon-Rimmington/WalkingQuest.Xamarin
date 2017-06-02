namespace WalkingQuest.Droid.Database.Objects
{
    public class Character
    {
        /*      TODO
            -   Crowd-funding Incentives
                -   Set up a CharacterClass object so that
            
                TODO
            -   General
                -   Object references are placeholders; we should consider changing them to IDs if this proves to be too
                    much of a drain on the device
        */
        private string CharacterName { get; }
        private int Level { get; }
        private Inventory Inventory { get; }
        private long CurrGold { get; }

        private long CurrExp { get; }
        private long ExpForNext { get; }

        public Character(string characterName, int level, Inventory inventory, long currGold, long currExp, long expForNext)
        {
            CharacterName = characterName;
            Level = level;
            Inventory = inventory;
            CurrGold = currGold;
            CurrExp = currExp;
            ExpForNext = expForNext;
        }
    }
}