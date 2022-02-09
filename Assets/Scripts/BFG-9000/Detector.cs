using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkClass
{
    public GameObject link;
    public Connector connector;
    public string targetName;
}

public class Detector : MonoBehaviour
{
    public GameObject linkPrefab;

    private List<LinkClass> linksList = new List<LinkClass>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (linkPrefab != null)
            {
                LinkClass newLink = new LinkClass() { link = Instantiate(linkPrefab) as GameObject };
                newLink.connector = newLink.link.GetComponent<Connector>();
                newLink.targetName = other.name;
                linksList.Add(newLink);

                if (newLink.connector != null)
                {
                    newLink.connector.MakeConnection(transform.position, other.transform.position);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (linksList.Count > 0 && other.gameObject.tag == "Enemy")
        {
            for (int i = 0; i < linksList.Count; i++)
            {
                if (other.name == linksList[i].targetName)
                    linksList[i].connector.MakeConnection(transform.position, other.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (linksList.Count > 0)
        {
            for (int i = 0; i < linksList.Count; i++)
            {
                if (other.name == linksList[i].targetName)
                {
                    Destroy(linksList[i].link);
                    linksList.Remove(linksList[i]);
                }
            }
        }
    }

    public void DestroyAllLinks()
    {
        if (linksList.Count > 0)
        {
            for (int i = 0; i < linksList.Count; i++)
            {
                Destroy(linksList[i].link);
            }
        }
    }
}
