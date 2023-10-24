using Serilog;
using System.Collections.Generic;

namespace Preditor
{
	public class Event
	{
		public string Name;

		public Event()
		{

		}
	}

	public class EventData
	{
		public EventData()
		{

		}
	}

	public class EventDispatcher
	{
		private List<Event> _events;

		public EventDispatcher()
		{
			_events = new List<Event>();

			Log.Debug("EventDispatcher startup");
		}
	}
}

