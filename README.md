BotBits.Old
--------------

BotBits is great for building bots for EverybodyEdits, but what about the newly added [old.everybodyedits.com](http://old.everybodyedits.com/) game?  
Bots have always been fun because they've dealt with the many limitations the game had and it will be very interesting to see what people can come up with in the old everybodyedits where many features of modern EE are unavailable.  

BotBits.Old acts as an extension to the existing BotBits framework. This means that bots written in the original BotBits framework can work with BotBits.Old after changing two or three lines of code!  

Instead of:
```csharp
ConnectionManager
   .Of(bot)
   .EmailLogin("email", "pass")
   .CreateJoinRoom("room");
```

Write:  
```csharp
BotBitsOldExtension
    .LoadInto(bot); // Load the extension

OldConnectionManager
    .Of(bot)
    .Connect() // There is no login
    .CreateJoinRoom(0, 1); // Join level at row 0, column 1
```

...and you're done!

## Blocks
There is no message delay in old.everybodyedits.com so you can upload worlds as fast as your internet connection allows! The default blocks API can be used (however, there is no support for Placers).

Background blocks do not exist in Old, and Foreground blocks have different Ids. BotBits.Old provides you with a new class `OldBlock` which can be used instead of `Foreground`.

To fill a 50x50 area:

```csharp
Blocks.Of(bot).In(new Rectangle(0, 0, 50, 50)).Set(OldBlock.Basic.Green);
```
