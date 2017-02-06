namespace Klockmann.BotHelper.Channels
{
    using Microsoft.Bot.Connector;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class Telegram
    {
        public static object SendSticker(string stickerId)
        {
            return JObject.FromObject(
                new
                {
                    method = "sendSticker",
                    parameters = new
                    {
                        sticker = stickerId
                    }
                });
        }

        public static object SendSticker(string url, string mediaType)
        {
            return JObject.FromObject(
                new
                {
                    method = "sendSticker",
                    parameters = new
                    {
                        sticker = new
                        {
                            url = url,
                            mediaType = mediaType
                        }
                    }
                });
        }

        public static string GetSticker(Activity activity)
        {
            try
            {
                var json = activity.ChannelData as JObject;
                var token = json.SelectToken("message.sticker.file_id");
                return token.Value<string>();
            }
            catch
            {
                return null;
            }
        }
    }
}