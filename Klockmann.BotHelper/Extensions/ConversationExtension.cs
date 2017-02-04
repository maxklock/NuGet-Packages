namespace Klockmann.BotHelper.Extensions
{
    using System.Threading.Tasks;

    using Microsoft.Bot.Connector;

    public static class ConversationsExtension
    {
        public static Task<ResourceResponse> SendToConversationAsync(this IConversations conversations, string text, string channelId, string conversationId, ChannelAccount from)
        {
            return conversations.SendToConversationAsync(new Activity
            {
                Type = ActivityTypes.Message,
                ChannelId = channelId,
                Conversation = new ConversationAccount(id: conversationId),
                From = from,
                Text = text
            });
        }
    }
}