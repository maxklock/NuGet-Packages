namespace Klockmann.BotHelper
{
    public class CommandHelp
    {
        #region constructors and destructors

        public CommandHelp() : this(string.Empty)
        {
        }

        public CommandHelp(string description, params string[] parameters)
        {
            Description = description;
            Parameters = parameters;
        }

        #endregion

        #region properties

        public string Description { get; set; }

        public string[] Parameters { get; set; }

        #endregion
    }
}