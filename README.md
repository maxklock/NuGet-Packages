# Klockmann Nuget Packages

## Klockmann.BotHelper
[![build](https://klockmann.visualstudio.com/_apis/public/build/definitions/0bc93951-0156-493b-9a24-221b9ee1eb1b/15/badge)](https://www.nuget.org/packages/Klockmann.BotHelper/)

Some Classes to create a command driven bot with the Microsoft BotFramework and some helpfull extensions.

#### MarkdownExtension

Style a given string with Markdown.

`"Bold".MakeBold()` => **Bold**  
`"Italic".MakeItalic()` => *Italic*  
`"Code".MakeInlineCode()` => `Code`  
`"http://github.com".MakeHyperlink("github")` => [github](http://github.com)
`"line".AddLineBreak()` Creates LineBreak after line

#### Command Handling

    var testCommands = new CommandList()
    {
        {
            "command1", new Parameters(
                "command1-no",
                async c =>
                {
                    await c.Activity.GetConnectorClient().Conversations.ReplyToActivityAsync(c.Activity.CreateReply("1: No Parameter"));
                })
        },
        {
            "command1", new Parameters(
                "comand1-two",
                async c =>
                {
                    await c.Activity.GetConnectorClient().Conversations.ReplyToActivityAsync(c.Activity.CreateReply("1: Two Parameters: " + c.Parameters[0] + " " + c.Parameters[1]));
                },
                ParameterType.Integer,
                ParameterType.Boolean)
        },
        {
            "command2", new Parameters(
                "command2",
                async c =>
                {
                    await c.Activity.GetConnectorClient().Conversations.ReplyToActivityAsync(c.Activity.CreateReply("2: No Parameter"));
                })
        },
    };

    if (!await testCommands.InvokeAsync(activity))
    {
        await activity.GetConnectorClient().Conversations.ReplyToActivityAsync(activity.CreateReply("No match!"));
    }

#### BotStateExtension

    var data = activity.GetStateClient().BotState.GetUserDataProperty<PropertyType>(activity.ChannelId, activity.From.Id, "propertyKey");

    activity.GetStateClient().BotState.SetUserDataProperty(activity.ChannelId, activity.From.Id, "propertyKey", data);

#### Telegram Channel

    var sticker = Telegram.GetSticker(activity);
    if (sticker != null)
    {
        var reply = activity.CreateReply();
        reply.ChannelData = Telegram.SendSticker(sticker);
        await activity.GetConnectorClient().Conversations.ReplyToActivityAsync(reply);
    }

    var location = Telegram.GetLocation(activity);
    if (location != null)
    {
        var reply = activity.CreateReply();
        reply.ChannelData = Telegram.SendLocation(location);
        await activity.GetConnectorClient().Conversations.ReplyToActivityAsync(reply);
    }
    
    var contact = Telegram.GetContact(activity);
    if (contact != null)
    {
        var reply = activity.CreateReply();
        reply.ChannelData = Telegram.SendContact(contact);
        await activity.GetConnectorClient().Conversations.ReplyToActivityAsync(reply);
    }

    var reply = activity.CreateReply();
    reply.ChannelData = Telegram.EditMessage(messageIdToChange, "New Message");
    await activity.GetConnectorClient().Conversations.ReplyToActivityAsync(reply);

## Klockmann.Parsing
[![build](https://klockmann.visualstudio.com/_apis/public/build/definitions/0bc93951-0156-493b-9a24-221b9ee1eb1b/14/badge)](https://www.nuget.org/packages/Klockmann.Parsing/)

Some Classes to parse csv, html or xml files.

#### HtmlParser

    var client = new WebClient
    client.Encoding = Encoding.UTF8;
    var site = client.DownloadString("http://github.com");

    var parsed = HtmlParser.ParseHtml(site);

    // Get string in title
    var tag = parsed.GetTagsWithTag("title").First();
    var title = tag.ChildContent.First();