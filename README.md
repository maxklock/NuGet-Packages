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

    var commands = new CommandList
    {
        new Command("command1",
            async (a, c) =>
            {
                await a.GetConnectorClient().Conversations.ReplyToActivityAsync(a.CreateReply("You send command " + "one".MakeBold() + "."));
            }),
        new Command("command2",
            async (a, c) =>
            {
                await a.GetConnectorClient().Conversations.ReplyToActivityAsync(a.CreateReply("You send command " + "one".MakeBold() + "."));
            })
    };

    if (commands.MatchActivity(activity))
        await commands.InvokeCallback(activity);
    else if (activity.Text != null)
        await connector.Conversations.ReplyToActivityAsync(activity.CreateReply("Unkown command!".AddLineBreak() + commands.CreateHelpString()));

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