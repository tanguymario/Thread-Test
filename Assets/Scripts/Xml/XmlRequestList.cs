using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Xml request list.
/// </summary>
public class XmlRequestList : XmlRequest 
{
    #region Properties

    /// <summary>
    /// The node list.
    /// </summary>
    protected XmlNodeList _nodeList;
    public XmlNodeList nodeList
    {
        get { return _nodeList; }
        private set { _nodeList = value; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlRequestList"/> class.
    /// </summary>
    /// <param name="xmlDoc">Xml document.</param>
    /// <param name="xpath">Xpath.</param>
    public XmlRequestList(XmlDocument xmlDoc, string xpath) 
    : base(xmlDoc, xpath)
    {
        _nodeList = nodeList;
    }

    /// <summary>
    /// Implemented work function of the thread.
    /// </summary>
    protected override void ThreadMain()
    {
        lock (lockObject)
        {
            try
            {
                _nodeList = xmlDoc.SelectNodes(_xpath);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

	#endregion
}
