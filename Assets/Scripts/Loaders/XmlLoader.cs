using System.Threading;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;

/// <summary>
/// Xml loader.
/// </summary>
public class XmlLoader : FileLoader
{
    #region Properties

    /// <summary>
    /// The xml document.
    /// </summary>
    protected XmlDocument _xmlDoc;
    public XmlDocument xmlDoc
    {
        get { return _xmlDoc; }
        private set { _xmlDoc = value; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlLoader"/> class.
    /// </summary>
    /// <param name="path">Path.</param>
    /// <param name="xmlDoc">Xml document.</param>
    public XmlLoader(string path, ref XmlDocument xmlDoc) : base(path)
    {
        _xmlDoc = xmlDoc;
    }

    /// <summary>
    /// Implemented work function of the thread.
    /// </summary>
    protected override void ThreadMain()
    {
        base.ThreadMain();
        LoadXml();
    }

    /// <summary>
    /// Loads xml from a string.
    /// </summary>
    private void LoadXml()
    {
        if (content.IsNull())
        {
            return;
        }

        try
        {
            _xmlDoc.LoadXml(content);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion
}
