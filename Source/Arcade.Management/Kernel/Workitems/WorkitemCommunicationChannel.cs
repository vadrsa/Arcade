using System;
using System.Reactive.Subjects;

namespace Kernel.Workitems
{
    /// <summary>
    /// The communication channel implmentation for WorkitemBase
    /// </summary>
    internal class WorkitemCommunicationChannel : ISubject<WorkitemEventArgs>, IDisposable
    {
        /// <summary>
        /// The underlying subject
        /// </summary>
        ReplaySubject<WorkitemEventArgs> _subject = new ReplaySubject<WorkitemEventArgs>();

        public WorkitemCommunicationChannel()
        {
            // Set ReplaySubject so that if workitem is opened in modal dialog mode 
            // the creator of the workitem gets all massages when the execution context gets to it
            // this is assuming the workitem won't pass more than 10 messages
            _subject = new ReplaySubject<WorkitemEventArgs>(10);
        }

        /// <summary>
        /// Dispose of the channel
        /// </summary>
        public void Dispose()
        {
            _subject.Dispose();
        }

        public void OnCompleted()
        {
            _subject.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _subject.OnError(error);
        }

        public void OnNext(WorkitemEventArgs value)
        {
            _subject.OnNext(value);
        }

        IDisposable IObservable<WorkitemEventArgs>.Subscribe(IObserver<WorkitemEventArgs> observer)
        {
            return _subject.Subscribe(observer);
        }
    }
}
