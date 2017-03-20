﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;

namespace eoTouchDelivery.Core.Collections
{
    /// <summary>
    ///     Provides an ObservableCollection which is backed by an asynchronous "fill"
    ///     method. You can then "refresh" the data at any time and have the collection
    ///     make callbacks when starting and completing the refresh.
    /// </summary>
    [DebuggerDisplay("Count={Count}")]
	public class RefreshingCollection<T> : ObservableCollection<T>
	{
		readonly Func<Task<IEnumerable<T>>> _refreshDataFunc;
		bool _isRefreshing;

	    /// <summary>
	    ///     Create a new Refreshing Collection.
	    /// </summary>
	    /// <param name="refreshFunc">Method which returns the data for the collection</param>
	    public RefreshingCollection(Func<Task<IEnumerable<T>>> refreshFunc)
		{
			_refreshDataFunc = refreshFunc ?? throw new ArgumentNullException(nameof(refreshFunc));
		}

	    /// <summary>
	    ///     This delegate is called BEFORE a refresh is initated
	    /// </summary>
	    /// <value>The before refresh.</value>
	    public Func<RefreshingCollection<T>, object> BeforeRefresh { get; set; }

	    /// <summary>
	    ///     This delegate is called AFTER a refresh completes and the contents are replaced.
	    /// </summary>
	    /// <value>The after refresh.</value>
	    public Action<RefreshingCollection<T>, object> AfterRefresh { get; set; }

	    /// <summary>
	    ///     This delegate is called if a refresh throws an exception.
	    /// </summary>
	    /// <value>The refresh failed.</value>
	    public Func<RefreshingCollection<T>, Exception, Task> RefreshFailed { get; set; }

	    /// <summary>
	    ///     Called when the collection is being changed; we turn this off during
	    ///     full refresh events.
	    /// </summary>
	    /// <param name="e">E.</param>
	    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (!_isRefreshing)
				base.OnCollectionChanged(e);
		}

	    /// <summary>
	    ///     Refreshes the data in the collection. The refresh method is invoked and
	    ///     this method will replace all the data in the collection with the data coming
	    ///     back from the refresh method.
	    /// </summary>
	    /// <returns>Awaitable task</returns>
	    public async Task RefreshAsync(bool appendData = false)
		{
			object refreshParameter = null;
			Exception caughtException = null;
			_isRefreshing = true;

			try
			{
				if (BeforeRefresh != null)
					refreshParameter = BeforeRefresh.Invoke(this);

				var results = await _refreshDataFunc();
				if (results != null)
				{
					Clear();
					foreach (var item in results)
						Add(item);
				}
			}
			catch (Exception ex)
			{
				caughtException = ex;
			}

			if (caughtException != null)
			{
				if (RefreshFailed != null)
					await RefreshFailed.Invoke(this, caughtException);
			}
			else
			{
				AfterRefresh?.Invoke(this, refreshParameter);
			}

			// Done refresh the world.
			_isRefreshing = false;
			OnCollectionChanged(
				new NotifyCollectionChangedEventArgs(
					NotifyCollectionChangedAction.Reset));
		}
	}
}