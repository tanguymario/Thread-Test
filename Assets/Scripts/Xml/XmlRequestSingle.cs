using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Xml request single.
/// </summary>
public class XmlRequestSingle : XmlRequest 
{
    #region Properties

    /// <summary>
    /// The node.
    /// </summary>
    protected XmlNode _node;
    public XmlNode node
    {
        get { return _node; }
        private set { _node = value; } 
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlRequestSingle"/> class.
    /// </summary>
    /// <param name="xmlDoc">Xml document.</param>
    /// <param name="xpath">Xpath.</param>
    public XmlRequestSingle(XmlDocument xmlDoc, string xpath)
    :base(xmlDoc, xpath)
    {
        _node = node;
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
                _node = _xmlDoc.SelectSingleNode(_xpath);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    #endregion
}
