namespace Klockmann.BotHelper.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Bot.Connector;

    public static class CommandExtension
    {
        #region methods

        public static string CreateHelpString(this IEnumerable<Command> commands)
        {
            var lines = commands.Select(
                c =>
                {
                    var str = ActivityExtension.CommandPrefix + c.Name;
                    str.MakeBold();
                    if (c.Help != null)
                    {
                        str += " - " + c.Help.Description;
                    }
                    return str;
                }).ToList();

            return lines.Aggregate(
                (res, str) =>
                {
                    if (str != lines.First())
                    {
                        res.AddLineBreak();
                    }
                    return res + str;
                });
        }

        public static string CreateHelpString(this Command command)
        {
            var str = ActivityExtension.CommandPrefix + command.Name;
            str.MakeBold();
            if (command.Help == null)
            {
                return str;
            }

            str += " - " + command.Help.Description;
            foreach (var parameter in command.Help.Parameters)
            {
                str.AddLineBreak();
                str += parameter;
            }
            return str;
        }

        public static Task InvokeCallback(this IEnumerable<Command> commands, Activity activity)
        {
            return Task.Run(
                () =>
                {
                    var match = commands.FirstOrDefault(c => c.MatchActivity(activity));
                    match?.Callback.Invoke(activity, match);
                });
        }

        public static bool MatchActivity(this Command command, Activity activity)
        {
            var actCommand = activity.GetCommand();
            return command.Name == actCommand;
        }

        public static bool MatchActivity(this IEnumerable<Command> commands, Activity activity)
        {
            return commands.Any(c => c.MatchActivity(activity));
        }

        #endregion
    }
}