namespace Klockmann.BotHelper
{
    using System;

    using Microsoft.Bot.Connector;

    public class Command
    {
        #region constructors and destructors

        public Command() : this("command", null)
        {
        }

        public Command(string name, Action<Activity, Command> callback) : this(name, callback, null)
        {
            
        }

        public Command(string name, Action<Activity, Command> callback, CommandHelp help)
        {
            Name = name;
            Callback = callback;
            Help = help;
        }

        #endregion

        #region properties

        public Action<Activity, Command> Callback { get; set; }

        public string Name { get; set; }

        public CommandHelp Help { get; set; }

        #endregion
    }
}