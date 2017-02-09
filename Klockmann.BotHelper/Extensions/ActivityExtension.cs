namespace Klockmann.BotHelper.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Bot.Connector;

    public static class ActivityExtension
    {
        #region constants

        private static readonly List<ConnectorClient> ConnectorClients = new List<ConnectorClient>();

        #endregion

        #region methods

        public static string GetCommand(this Activity activity)
        {
            if (activity.Text == null) return null;
            var command = activity.Text.Split(SplitCharacter)[0];
            return !command.StartsWith(CommandPrefix) ? null : command.Split(MentionCharacter)[0].Substring(CommandPrefix.Length);
        }

        public static string GetCommandMention(this Activity activity)
        {
            var command = activity.Text.Split(SplitCharacter)[0];
            if (!command.StartsWith(CommandPrefix))
            {
                return null;
            }
            var parts = command.Split(MentionCharacter);
            return parts.Length == 1 ? null : parts[1];
        }

        public static ConnectorClient GetConnectorClient(this Activity activity)
        {
            var client = ConnectorClients.FirstOrDefault(c => c.BaseUri.OriginalString == activity.ServiceUrl);
            if (client != null)
            {
                return client;
            }
            client = new ConnectorClient(new Uri(activity.ServiceUrl));
            ConnectorClients.Add(client);
            return client;
        }

        public static string[] GetParameters(this Activity activity)
        {
            var parts = activity.Text.Split(SplitCharacter).Skip(1).ToList();
            var parameters = new List<string>();
            
            while (parts.Any())
            {
                var part = parts.First();
                if (part.StartsWith("\""))
                {
                    var str = part.Substring(1);
                    while (!parts.First().EndsWith("\""))
                    {
                        parts.RemoveAt(0);
                        str += " " + parts.First();
                    }
                    str = str.Substring(0, str.Length - 1);
                    parameters.Add(str);
                }
                else
                {
                    parameters.Add(part);
                }
                parts.RemoveAt(0);
            }

            return parameters.ToArray();
        }

        public static bool MatchMention(this Activity activity, string mention)
        {
            var commandMention = GetCommandMention(activity);
            var parts = activity.Text.Split(SplitCharacter).Where(p => p.StartsWith(MentionCharacter.ToString()));

            return commandMention == mention || parts.Any(p => p == MentionCharacter + mention);
        }

        public static void Setup(string commandPrefix, char mentionCharacter, char splitCharacter)
        {
            CommandPrefix = commandPrefix;
            MentionCharacter = mentionCharacter;
            SplitCharacter = splitCharacter;
        }

        #endregion

        #region properties

        public static string CommandPrefix { get; set; } = "/";

        public static char MentionCharacter { get; set; } = '@';

        public static char SplitCharacter { get; set; } = ' ';

        #endregion
    }
}