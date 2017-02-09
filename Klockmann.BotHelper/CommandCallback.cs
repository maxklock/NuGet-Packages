namespace Klockmann.BotHelper
{
    using Microsoft.Bot.Connector;

    public struct CommandCallback
    {
        #region properties

        public Activity Activity { get; set; }

        public string Command { get; set; }

        public string ParameterListId { get; set; }

        public object[] Parameters { get; set; }

        #endregion
    }
}