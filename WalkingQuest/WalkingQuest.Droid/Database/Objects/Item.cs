using Org.Json;

namespace WalkingQuest.Droid.Database.Objects
{
    public class Item
    {
        private int GoldValue { get; }
        private string Name { get; }

        private JSONObject Attributes { get; }

        public Item(int goldValue, string name, JSONObject attributes)
        {
            this.GoldValue = goldValue;
            this.Name = name;
            this.Attributes = attributes ?? new JSONObject();
        }
    }
}