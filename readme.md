LCSK (LiveChat Starter Kit) - Logs to Sql Server
=============================

LCSK is a simple, lightweight live chat / live support ASP.NET C# app. It uses SignalR for the communication channel 
between the website visitors and the agent(s). No database are required.

This tool was developed originally by Dominic St. Pierre and can be found here: https://github.com/dstpierre/lcsk

He gets all the credit for a nice little tool.  This fork allows you to actually store the chat messages into a sql implementation.

Once you fork and clone the project you will want to execute the .sql file directly within SQL Server to create the table structure.

Upon that, take a look at the chat.js file, here inside the addMessage I have made an ajax call to insert the message.

You will need to write your own server side asmx to handle the insert...mine was very simple with the following signature:

`public void AddChatMessage(string chatId, string from, string msg)`