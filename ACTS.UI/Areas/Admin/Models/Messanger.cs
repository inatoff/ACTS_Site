using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTS.UI.Areas.Admin.Models
{
	public enum MessageType: byte
	{
		Danger,
		Warning,
		Info,
		Success
	}

	public class Messenger : SortedSet<Message>
	{
		public Messenger(IEnumerable<Message> messages)
			: base(messages)
		{
		}

		public Messenger()
			: base()
		{
		}

		public const string Name = "Messenger"; 
	}

	public struct Message : IComparable<Message>
	{
		public MessageType Type;
		public string Title;
		public string Body;

		public Message(MessageType type, string body)
		{
			Type = type;
			Title = "Alert!";
			Body = body;
		}

		public Message(MessageType type, string title, string body)
		{
			Type = type;
			Title = title;
			Body = body;
		}


		public int CompareTo(Message other)
		{
			var result = ((IComparable)Type).CompareTo(other.Type);

			if (result == 0)
			{
				result = Title.CompareTo(other.Title);

				if (result == 0)
					result = Body.CompareTo(other.Body);
			}
			return result;
		}
	}
}
