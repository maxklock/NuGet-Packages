namespace Klockmann.BotHelper.Channels
{
    using System;

    using Klockmann.BotHelper.Types.Telegram;

    using Microsoft.Bot.Connector;

    using Newtonsoft.Json.Linq;

    public static class Telegram
    {
        #region methods

        public static Contact GetContact(Activity activity)
        {
            try
            {
                var json = activity.ChannelData as JObject;
                var phone = json?.SelectToken("message.contact.phone_number");
                var first = json?.SelectToken("message.contact.first_name");
                var last = json?.SelectToken("message.contact.last_name");
                return new Contact
                {
                    Phone = phone?.Value<string>(),
                    FirstName = first?.Value<string>(),
                    LastName = last?.Value<string>()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Location GetLocation(Activity activity)
        {
            try
            {
                var json = activity.ChannelData as JObject;
                var latitudeToken = json.SelectToken("message.location.latitude");
                var longitudeToken = json.SelectToken("message.location.longitude");
                return new Location
                {
                    Latitude = latitudeToken.Value<float>(),
                    Longitude = longitudeToken.Value<float>()
                };
            }
            catch (Exception)
            {
                return null;
            }
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

        public static object SendChatAction(string action)
        {
            return JObject.FromObject(
                new
                {
                    method = "sendChatAction",
                    parameters = new
                    {
                        action
                    }
                });
        }

        public static object SendContact(Contact contact)
        {
            return JObject.FromObject(
                new
                {
                    method = "sendContact",
                    parameters = new
                    {
                        phone_number = contact.Phone,
                        first_name = contact.FirstName,
                        last_name = contact.LastName
                    }
                });
        }

        public static object SendLocation(Location location)
        {
            return JObject.FromObject(
                new
                {
                    method = "sendLocation",
                    parameters = new
                    {
                        latitude = location.Latitude,
                        longitude = location.Longitude
                    }
                });
        }

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
                            url,
                            mediaType
                        }
                    }
                });
        }

        #endregion
    }
}