using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public abstract class XmlRequest : SampleThread
{
    #region Properties

    protected XmlDocument _xmlDoc;
    public XmlDocument xmlDoc
    {
        get { return _xmlDoc; }
        private set { _xmlDoc = value; }
    }

    protected string _xpath;
    public string xpath
    {
        get { return _xpath; }
        private set { _xpath = value; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlRequest"/> class.
    /// </summary>
    /// <param name="xmlDoc">Xml document.</param>
    /// <param name="xpath">Xpath.</param>
    public XmlRequest(XmlDocument xmlDoc, string xpath) : base()
    {
        if (xmlDoc.IsNull())
        {
            throw new System.ArgumentNullException("Xml Doc must be initialized");
        }

        if (string.IsNullOrEmpty(xpath))
        {
            throw new System.ArgumentException("xpath must not be null/empty");
        }

        _xmlDoc = xmlDoc;
        _xpath = xpath;
    }

    /// <summary>
    /// Implemented work function of the thread.
    /// </summary>
    protected abstract override void ThreadMain();

    #endregion
}
