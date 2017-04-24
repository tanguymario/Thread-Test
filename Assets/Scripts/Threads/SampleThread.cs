using System.Threading;
using System.Collections;
using System.IO;
using UnityEngine;

/// <summary>
/// Sample thread.
/// </summary>
public abstract class SampleThread 
{
    #region Properties

    /// <summary>
    /// The lock object (When opening a file, we must lock this file.
    /// So that, only this instance can read this file until the end.
    /// </summary>
    protected object lockObject = new Object();

    /// <summary>
    /// The thread.
    /// </summary>
    private Thread _thread;

    #endregion

    #region Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="SampleThread"/> class.
    /// </summary>
    public SampleThread()
    {
        _thread = new Thread(new ThreadStart(ThreadFunction));
    }

    /// <summary>
    /// Main work of the thread.
    /// </summary>
    protected void ThreadFunction()
    {
        ThreadMain();
        Abort();
    }

    /// <summary>
    /// Implemented work function of the thread.
    /// </summary>
    protected abstract void ThreadMain();

    /// <summary>
    /// Abort this instance.
    /// </summary>
    protected void Abort()
    {
        _thread.Abort();
    }

    #endregion

    #region Coroutines

    /// <summary>
    /// Load content from the file into the 'content'.
    /// </summary>
    public IEnumerator Start()
    {
        /* Check that the thread is not alive */
        if (_thread.IsAlive)
        {
            Debug.LogWarning("Sample Thread: Thread was alive!");
            yield break;
        }

        /* Start the thread */
        _thread.Start();

        /* Waiting while thread is working */
        while (_thread.IsAlive)
        {
            yield return null;
        }
    
        yield return null;
    }

    #endregion
}
