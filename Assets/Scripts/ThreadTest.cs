using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class ThreadTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Test());
    }


    IEnumerator Test()
    {
        yield return new WaitForSeconds(1f);

        UnityEngine.Debug.Log("Coroutine start");

        string path = string.Concat(Application.streamingAssetsPath, "/test.xml");
    
        UnityEngine.Debug.Log(path);
    
        float startTime = Time.time;

        XmlDocument xmlDoc = new XmlDocument();

        XmlLoader fileLoader = new XmlLoader(path, ref xmlDoc);

        yield return StartCoroutine(fileLoader.Start());

        UnityEngine.Debug.Log("Loaded");

        /*
        XmlRequestList xmlRequest = new XmlRequestList(xmlDoc, "//catalog/book/author");

        yield return StartCoroutine(xmlRequest.Start());

        foreach (XmlNode node in xmlRequest.nodeList)
        {
            UnityEngine.Debug.Log(node.InnerText);
        }
        */

        XmlRequestSingle xmlRequest = new XmlRequestSingle(xmlDoc, "//catalog/book[@id='bk101']/author");
        yield return StartCoroutine(xmlRequest.Start());

        // UnityEngine.Debug.Log(xmlDoc.SelectSingleNode("//catalog/book[@id='bk101']/author").InnerText);

        UnityEngine.Debug.Log("Coroutine end");

        yield return null;
    }
}
