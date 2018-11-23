using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager: MonoBehaviour {

    public static Queue<GameObject> partyMembers;
    public static GameObject[] members;
    private static bool canSwap;

    private void Start()
    {
        partyMembers = new Queue<GameObject>();
        int i = 0;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            partyMembers.Enqueue(player);
            Debug.Log("Added: " + player.name + " PartyNumber: " + i);
            player.GetComponent<Character3D>().partyNumber = i++;
        }
        members = new GameObject[] {new GameObject(), new GameObject(), new GameObject(), new GameObject(), new GameObject()};
        Debug.Log(partyMembers);
        partyMembers.CopyTo(members, 0);
        canSwap = true;
        StartCoroutine(WaitToSwap());
    }

    public static void SwapPartyMember()
    {
        if (canSwap)
        {
            partyMembers.Peek().GetComponent<Character3D>().partyNumber = partyMembers.Count - 1;
            GameObject lastMember = partyMembers.Dequeue();
            foreach (GameObject member in partyMembers)
            {
                member.GetComponent<Character3D>().partyNumber--;
            }
            partyMembers.Enqueue(lastMember);
            partyMembers.CopyTo(members, 0);
            canSwap = false;
        }
    }

    IEnumerator WaitToSwap()
    {
        for (; ; )
        {
            canSwap = true;
            yield return new WaitForSeconds(1f);
        }
    }

}
