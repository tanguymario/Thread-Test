using System.Threading;
using System.Collections;
using System.IO;
using UnityEngine;

/// <summary>
/// File loader.
/// </summary>
public class FileLoader : SampleThread
{
    #region Properties

    /// <summary>
    /// File path.
    /// </summary>
    protected string _path;
    public string path { get { return _path; } }

    /// <summary>
    /// Content of the file.
    /// </summary>
    private string _content;
    public string content { get { return _content; } private set { _content = value; } }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="FileLoader"/> class.
    /// </summary>
    /// <param name="path">File path.</param>
    public FileLoader(string path) : base()
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new System.ArgumentNullException("path");
        }
    
        _path = path;
    }

    /// <summary>
    /// Implemented work function of the thread.
    /// </summary>
    protected override void ThreadMain()
    {
        LoadDataFromFile();
    }

    /// <summary>
    /// Loads the data from file.
    /// </summary>
    protected virtual void LoadDataFromFile()
    {
        if (string.IsNullOrEmpty(_path))
        {
            return;
        }

        lock (lockObject)
        {
            try
            {
                StreamReader reader = new StreamReader(_path);

                _content = reader.ReadToEnd();
                
                reader.Close();
                reader.Dispose();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    #endregion

    #region Coroutines

    #endregion
}
