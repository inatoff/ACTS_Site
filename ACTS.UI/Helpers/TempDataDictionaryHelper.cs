using ACTS.UI.Areas.Admin.Models;
using ACTS.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACTS.UI.Helpers
{
	public static class TempDataDictionaryHelper
	{
		public static void AddMessage(this TempDataDictionary tempData, MessageType type, string body)
		{
			var message = new Message(type, body);

			dynamic messenger;
			if (tempData.TryGetValue(Messenger.Name, out messenger))
				messenger.Add(message);
			else
			{
				messenger = new Messenger();
				messenger.Add(message);
				tempData[Messenger.Name] = messenger;
			}
		}

		public static void AddMessage(this TempDataDictionary tempData, MessageType type, string title, string body)
		{
			var message = new Message(type, title, body);

			dynamic messenger;
			if (tempData.TryGetValue(Messenger.Name, out messenger))
				messenger.Add(message);
			else
			{
				messenger = new Messenger();
				messenger.Add(message);
				tempData[Messenger.Name] = messenger;
			}
		}

		public static void AddMessage(this TempDataDictionary tempData, Message message)
		{
			dynamic messenger;
			if (tempData.TryGetValue(Messenger.Name, out messenger))
				messenger.Add(message);
			else
			{
				messenger = new Messenger();
				messenger.Add(message);
				tempData[Messenger.Name] = messenger;
			}
		}

		public static void AddMessage(this TempDataDictionary tempData, MessageType type, IEnumerable<string> bodies)
		{
			var messages = bodies.Select(b => new Message(type, b));

			dynamic messenger;
			if (tempData.TryGetValue(Messenger.Name, out messenger))
				foreach (var message in messages)
					messenger.Add(message);
			else
			{
				messenger = new Messenger(messages);
				tempData[Messenger.Name] = messenger;
			}
		}
	}
}
