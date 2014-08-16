namespace GameLogic.Characters.Bots
{
    public abstract class Bot : Character, IBot
    {
        protected Bot()
        {
            SetName(BotHelper.GenerateRandomBotName());
        }

        public abstract int Worth { get; }
    }
}
