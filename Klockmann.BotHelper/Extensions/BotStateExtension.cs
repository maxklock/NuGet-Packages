namespace Klockmann.BotHelper.Extensions
{
    using System.Threading.Tasks;

    using Microsoft.Bot.Connector;

    public static class BotStateExtension
    {
        #region methods

        public static TData GetConversationDataProperty<TData>(this IBotState state, string channelId, string conversationId, string property)
        {
            var userData = state.GetConversationData(channelId, conversationId);
            return userData.GetProperty<TData>(property);
        }

        public static async Task<TData> GetConversationDataPropertyAsync<TData>(this IBotState state, string channelId, string conversationId, string property)
        {
            var userData = await state.GetConversationDataAsync(channelId, conversationId);
            return userData.GetProperty<TData>(property);
        }

        public static TData GetUserDataProperty<TData>(this IBotState state, string channelId, string userId, string property)
        {
            var userData = state.GetUserData(channelId, userId);
            return userData.GetProperty<TData>(property);
        }

        public static async Task<TData> GetUserDataPropertyAsync<TData>(this IBotState state, string channelId, string userId, string property)
        {
            var userData = await state.GetUserDataAsync(channelId, userId);
            return userData.GetProperty<TData>(property);
        }

        public static void SetConversationDataProperty<TData>(this IBotState state, string channelId, string conversationId, string property, TData data)
        {
            var userData = state.GetConversationData(channelId, conversationId);
            userData.SetProperty(property, data);
            state.SetConversationData(channelId, conversationId, userData);
        }

        public static async Task SetConversationDataPropertyAsync<TData>(this IBotState state, string channelId, string conversationId, string property, TData data)
        {
            var userData = await state.GetConversationDataAsync(channelId, conversationId);
            userData.SetProperty(property, data);
            await state.SetConversationDataAsync(channelId, conversationId, userData);
        }

        public static void SetUserDataProperty<TData>(this IBotState state, string channelId, string userId, string property, TData data)
        {
            var userData = state.GetUserData(channelId, userId);
            userData.SetProperty(property, data);
            state.SetUserData(channelId, userId, userData);
        }

        public static async Task SetUserDataPropertyAsync<TData>(this IBotState state, string channelId, string userId, string property, TData data)
        {
            var userData = await state.GetUserDataAsync(channelId, userId);
            userData.SetProperty(property, data);
            await state.SetUserDataAsync(channelId, userId, userData);
        }

        #endregion
    }
}