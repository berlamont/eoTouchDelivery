using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace eoTouchDelivery.Core.Collections
{
    /// <summary>
    ///     ObservableCollection implementation which supports
    ///     turning off notifications for mass updates through
    ///     the <see cref="BeginMassUpdate" /> method.
    /// </summary>
    /// <example>
    ///     <code>
    /// var coll = new OptimizedObservableCollection&lt;string&gt;();
    /// ...
    /// using (BeginMassUpdate ()) {
    ///    foreach (var value in names)
    ///       coll.Add (value);
    /// }
    /// </code>
    /// </example>
    [DebuggerDisplay("Count={Count}")]
	public class OptimizedObservableCollection<T> : ObservableCollection<T>
	{
		bool _shouldRaiseNotifications;

	    /// <summary>
	    ///     Init a new instance of the collection.
	    /// </summary>
	    public OptimizedObservableCollection()
		{
		}

	    /// <summary>
	    ///     Initialize a new instance of the collection from an existing data set.
	    /// </summary>
	    /// <param name="collection">Collection.</param>
	    public OptimizedObservableCollection(IEnumerable<T> collection)
			: base(collection)
		{
		}

	    /// <summary>
	    ///     This method turns off notifications until the returned object
	    ///     is Disposed. At that point, the entire collection is invalidated.
	    /// </summary>
	    /// <returns>IDisposable</returns>
	    public IDisposable BeginMassUpdate() => new MassUpdater(this);

	    /// <summary>
	    ///     Turn off the collection changed notification
	    /// </summary>
	    /// <param name="e">E.</param>
	    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (_shouldRaiseNotifications)
				base.OnCollectionChanged(e);
		}

	    /// <summary>
	    ///     Turn off the property changed notification
	    /// </summary>
	    /// <param name="e">E.</param>
	    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (_shouldRaiseNotifications)
				base.OnPropertyChanged(e);
		}

	    /// <summary>
	    ///     IDisposable class which turns off updating
	    /// </summary>
	    class MassUpdater : IDisposable
		{
			readonly OptimizedObservableCollection<T> _parent;

			public MassUpdater(OptimizedObservableCollection<T> parent)
			{
				_parent = parent;
				parent._shouldRaiseNotifications = false;
			}


			public void Dispose()
			{
				_parent._shouldRaiseNotifications = true;
				_parent.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
				_parent.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
				_parent.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}

#if DEBUG

			~MassUpdater()
			{
				Debug.Assert(true, "Did not dispose returned object from OptimizedObservableCollection.BeginMassUpdate!");
			}

#endif
		}
	}
}