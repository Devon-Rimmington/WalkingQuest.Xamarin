namespace WalkingQuest.Droid.Database.Objects
{
    public class User
    {
        public string UserName { get; set; }

        public Character CurrCharacter;
        /*  TODO    -   When ready to implement multiple characters, create a collection of Character objects. May need
                        to limit this to a max amount (don't want to use too much memory for all the inventories and
                        stuff
                        */

        private Quest CurrQuest { get; set; }

        public User(Character currCharacter, string userName, Quest currQuest)
        {
            CurrCharacter = currCharacter;
            UserName = userName;
            CurrQuest = currQuest;
        }
    }
}