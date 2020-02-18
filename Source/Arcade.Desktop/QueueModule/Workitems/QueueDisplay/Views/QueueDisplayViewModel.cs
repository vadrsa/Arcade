using Infrastructure.Mvvm;
using QueueModule.Services;
using SharedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace QueueModule.Workitems.QueueDisplay.Views
{
    class QueueDisplayViewModel : WorkitemViewModel<QueueDisplayWorkitem>
    {
		public QueueDisplayViewModel()
		{
			var timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 15), DispatcherPriority.Normal, delegate
			{
				Load();
			}, Application.Current.Dispatcher);
			timer.Start();
		}

		private DateTime _currentEnd ;

		public DateTime CurrentEnd
		{
			get { return _currentEnd; }
			set { Set(ref _currentEnd, value, nameof(CurrentEnd));  }
		}

		private int _nextQueueNumber;

		public int NextQueueNumber
		{
			get { return _nextQueueNumber; }
			set { Set(ref _nextQueueNumber, value, nameof(NextQueueNumber)); }
		}

		private bool _hasNext;

		public bool HasNext
		{
			get { return _hasNext; }
			set { Set(ref _hasNext, value, nameof(HasNext)); }
		}

		private bool _IsOpen;

		public bool IsOpen
		{
			get { return _IsOpen; }
			set { Set(ref _IsOpen, value, nameof(IsOpen)); }
		}

		public string ComputerID { get; set; }

		protected async override Task DoLoad(CancellationToken token)
		{
			var queue = await new SessionService().GetQueue(ComputerID);
			ProcessQueue(queue);
		}

		private void ProcessQueue(ComputerQueueDto queue)
		{
			HasNext = false;
			CurrentEnd = default;
			if (queue.Current == null)
			{
				IsOpen = true;
			}
			else
			{
				IsOpen = false;
				var date = (DateTime)new UtcToLocalConverter().Convert(queue.Current.StartDate, null, null, null);
				CurrentEnd = date.AddMinutes(queue.Current.Duration);
				var firstInQueue = queue.Queue.OrderBy(s => s.QueueNumber).FirstOrDefault();
				if(firstInQueue != null)
				{
					NextQueueNumber = firstInQueue.QueueNumber;
					HasNext = true;
				}
			}

		}
	}
}
