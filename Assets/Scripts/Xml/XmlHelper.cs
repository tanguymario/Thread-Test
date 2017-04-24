using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

/// <summary>
/// Xml helper.
/// </summary>
public static class XmlHelper
{
    #region Creation

    /// <summary>
    /// Creates an xml document.
    /// </summary>
    /// <returns>The xml document created.</returns>
    /// <param name="root">The name of the Root node.</param>
    public static XmlDocument CreateXmlDocument(string root)
    {
        XmlDocument xmlDoc = new XmlDocument();

        XmlNode xmlHeader = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
        XmlNode xmlRootElement = xmlDoc.CreateElement(root);

        xmlDoc.AppendChild(xmlHeader);
        xmlDoc.AppendChild(xmlRootElement);

        return xmlDoc;
    }

    /// <summary>
    /// Creates an xml attribute to an xml node.
    /// </summary>
    /// <returns>The xml attribute created.</returns>
    /// <param name="xmlDoc">Xml document.</param>
    /// <param name="attrName">Attribute name.</param>
    /// <param name="attrValue">Attribute value.</param>
    public static XmlAttribute CreateXmlAttribute(
        XmlDocument xmlDoc, string attrName, string attrValue)
    {
        XmlAttribute attr = xmlDoc.CreateAttribute(attrName);
        attr.Value = attrValue;

        return attr;
    }

    /// <summary>
    /// Creates an xml node.
    /// </summary>
    /// <returns>The xml node.</returns>
    /// <param name="xmlDoc">Xml document.</param>
    /// <param name="nodeName">Name of the node.</param>
    public static XmlNode CreateXmlNode(XmlDocument xmlDoc, string nodeName)
    {
        if (!string.IsNullOrEmpty(nodeName))
            return xmlDoc.CreateElement(nodeName);

        Debug.LogWarning("Can't create an xml node with no name");
        return null;
    }

    /// <summary>
    /// Creates an xml node.
    /// </summary>
    /// <returns>The xml node.</returns>
    /// <param name="xmlDoc">Xml document.</param>
    /// <param name="nodeName">Name of the node.</param>
    /// <param name="nodeInnerText">InnerText of the node.</param> 
    public static XmlNode CreateXmlNode(
        XmlDocument xmlDoc, string nodeName, string nodeInnerText = null)
    {
        if (string.IsNullOrEmpty(nodeName))
        {
            Debug.LogWarning("Can't create an xml node with no name");
            return null;
        }

        XmlNode node = xmlDoc.CreateElement(nodeName);
        node.InnerText = nodeInnerText; 

        return node;
    }

    /// <summary>
    /// Creates an xml node.
    /// </summary>
    /// <returns>The xml node.</returns>
    /// <param name="xmlDoc">Xml document.</param>
    /// <param name="nodeName">Name of the node.</param>
    /// <param name="attributes">Attributes of the node.</param>
    public static XmlNode CreateXmlNode(
        XmlDocument xmlDoc, string nodeName, Dictionary<string, string> attributes)
    {
        XmlNode node = CreateXmlNode(xmlDoc, nodeName);
        if (!string.IsNullOrEmpty(nodeName))
        {
            node = xmlDoc.CreateElement(nodeName);
            if (attributes.Exists())
            {
                foreach (KeyValuePair<string, string> attribute in attributes)
                {
                    XmlAttribute attr = xmlDoc.CreateAttribute(attribute.Key);
                    attr.Value = attribute.Value;

                    node.Attributes.Append(attr);
                }
            }
        }

        return node;
    }

    #endregion

    #region Loading

    public static XmlDocument LoadFromRawData(string xml)
    {
        XmlDocument xmlDoc = new XmlDocument();

        if (string.IsNullOrEmpty(xml))
        {
            return null;
        }

        xmlDoc.LoadXml(xml);

        return xmlDoc;
    }



    /// <summary>
    /// Loads an xml file in the resources folder.
    /// </summary>
    /// <returns>The XmlDocument loaded.</returns>
    /// <param name="path">Path to load xml in resources.</param>
    public static XmlDocument LoadXmlInResources(string path)
    {
        XmlDocument xmlDoc = new XmlDocument();
    
        try
        {
            TextAsset textAsset = Resources.Load<TextAsset>(path);
            xmlDoc.LoadXml(textAsset.text);
        }
        catch
        {
            Debug.LogError(string.Concat("XML: File could not be loaded at : ", path));
            xmlDoc = null;
        }

        return xmlDoc;
    }

    /// <summary>
    /// Loads an xml document at absolute path.
    /// </summary>
    /// <returns>The xml document loaded.</returns>
    /// <param name="absolutePath">The Absolute path from computer root.</param>
    public static XmlDocument LoadXmlAtAbsolutePath(string absolutePath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        if (File.Exists(absolutePath))
        {
            xmlDoc.Load(absolutePath);
        }
        else
        {
            Debug.LogError(string.Concat(
                    "Xml file could not be loaded at : ", absolutePath));

            xmlDoc = null;
        }

        return xmlDoc;
    }

    public static IEnumerator LoadXmlAtAbsolutePathAsync(string path, XmlDocument xmlDoc)
    {
        if (xmlDoc.IsNull())
        {
            xmlDoc = new XmlDocument();
        }

        path = string.Concat("file://", path);

        string text = string.Empty;
        if (path.Contains("://"))
        {
            WWW www = new WWW(path);

            yield return www;
      
            text = www.text;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error);
            }
        }
        else
        {
            text = File.ReadAllText(path);
        }

        if (string.IsNullOrEmpty(text))
        {
            Debug.LogError(string.Concat("XML: Could not be loaded at : ", path));
            yield break;
        }
        else
        {
            try
            {
                xmlDoc.LoadXml(text);
            }
            catch (Exception e)
            {
                Debug.LogError(string.Concat("XML: File loaded at : ", path, " was not XML"));
                Debug.LogError(e.Message);
            }
        }

        yield return null;
    }

    #endregion

    #region Saving

    /// <summary>
    /// Saves the XML data.
    /// </summary>
    /// <returns><c>true</c>, if XML data was saved, <c>false</c> otherwise.</returns>
    /// <param name="xmlDoc">Xml document to save.</param>
    /// <param name="destination">Destination path.</param>
    public static bool SaveXMLData(XmlDocument xmlDoc, string destination)
    {
        bool saved = false;

        if (xmlDoc.Exists())
        {
            xmlDoc.Save(destination);        
            saved = File.Exists(destination);
        }
        else
        {
            Debug.LogError("XML Document in argument is null");
        }

        return saved;
    }

    #endregion

    #region Get And Set

    /// <summary>
    /// Gets the XML node attribute.
    /// </summary>
    /// <returns>The XML node attribute.</returns>
    /// <param name="node">Xml Node.</param>
    /// <param name="attribute">Name of the Xml Attribute.</param>
    public static string GetXMLNodeAttribute(XmlNode node, string attribute)
    {
        return node.AttributeValueNull(attribute);
    }

    /// <summary>
    /// Sets the XML node attribute.
    /// </summary>
    /// <param name="node">Node.</param>
    /// <param name="attribute">Xml Attribute.</param>
    /// <param name="value">New value.</param>
    public static bool SetXMLNodeAttribute(
        XmlNode node, string attribute, string value)
    {
        if (node.AttributeValueNull(attribute).Exists())
        {
            node.Attributes[attribute].Value = value;
            return true;
        }

        Debug.LogWarning(
            string.Concat(
                "Could not set attribute : attribute \"", attribute, "\" missing"));

        return false;
    }

    #endregion

    #region Element Utilities

    /// <summary>
    /// Get the value of the XmlElement.
    /// </summary>
    /// <returns>Returns the value of this XmlElement, else null.</returns>
    /// <param name="element">Xml Element.</param>
    private static string ElementValueNull(this XmlElement element)
    {
        return element.IsNull() ? string.Empty : element.Value; 
    }

    /// <summary>
    /// Get the value of the XmlNode.
    /// </summary>
    /// <returns>Returns the value of this XmlNode, else null.</returns>
    /// <param name="element">Xml Node.</param>
    private static string ElementValueNull(this XmlNode node)
    {
        return node.IsNull() ? string.Empty : node.Value; 
    }

    /// <summary>
    /// Get the Attribute value of an xml element.
    /// </summary>
    /// <returns>The attribute of the xml element else null.</returns>
    /// <param name="element">Xml Element.</param>
    /// <param name="attributeName">Attribute name.</param>
    private static string AttributeValueNull(
        this XmlElement element, string attributeName)
    {
        if (element.IsNull())
            return string.Empty;

        XmlAttribute attr = element.Attributes[attributeName];
        return attr.IsNull() ? string.Empty : attr.Value;
    }

    /// <summary>
    /// Get the Attribute value of an xml node.
    /// </summary>
    /// <returns>The attribute of the xml node else null.</returns>
    /// <param name="element">Xml Node.</param>
    /// <param name="attributeName">Attribute name.</param>
    private static string AttributeValueNull(
        this XmlNode node, string attributeName)
    {
        if (node.IsNull())
            return string.Empty;
		
        XmlAttribute attr = node.Attributes[attributeName];
        return attr.IsNull() ? string.Empty : attr.Value;
    }

    #endregion

    #region Append

    /// <summary>
    /// Appends the xml child nodes to the parent.
    /// </summary>
    /// <param name="parent">Parent Node.</param>
    /// <param name="child">Child Nodes.</param>
    public static void AppendChildNodes(this XmlNode parent, params XmlNode[] childs)
    {
        if (parent.IsNull())
        {
            Debug.LogWarning("Parent xmlNode is null");
            return;
        }

        if (childs.IsNull())
        {
            Debug.LogWarning("Childs xml Nodes is null");
            return;
        }

        foreach (XmlNode child in childs)
        {
            parent.AppendChild(child);
        }
    }

    #endregion

    #region Searching

    /// <summary>
    /// Search an xml node.
    /// </summary>
    /// <returns>
    /// The first XML node found with the attribute name and value, else null
    /// </returns>
    /// <param name="nodeList">Node list.</param>
    /// <param name="attributeName">Attribute name.</param>
    /// <param name="value">Value of the attribute.</param>
    public static XmlNode SearchXMLNode(
        XmlNodeList nodeList, string attributeName, string attributeValue)
    {
        foreach (XmlNode node in nodeList)
        {
            if (node.AttributeValueNull(attributeName) == attributeValue)
            {
                return node;
            }
        }

        return null;
    }

    #endregion
}
