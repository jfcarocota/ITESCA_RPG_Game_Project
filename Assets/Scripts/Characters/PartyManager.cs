using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using GameCore.MemorySystem;
public class PartyManager: MonoBehaviour {

    public static Queue<GameObject> partyMembers;
    public static GameObject[] members;
    private static bool canSwap;
    private static CinemachineVirtualCamera vCam;

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
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
        if (MemorySystem.loadGame)
        {
            //Esto tiene que estar en algun script que se ejecute cuando empieze el juego.
            GameData partyPos = MemorySystem.LoadData("party.data");
            for (int j = 0; j < partyPos.PosVectors.Length; j++)
            {
                members[j].transform.position = partyPos.PosVectors[j];
            }
            MemorySystem.loadGame = false;
        }
        vCam.Follow = vCam.LookAt = members[0].transform;
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

            //change virtual camera to new party leader
            /*
             * pues esta es una manera de cambiar de follow pero se hace un corte cuando cambia
             * la otra es usar una vCam para cada jugador y activarla cuando cambia de jugador,
             * con esto ultimo deveria hacer un cambio suave.
            */
            vCam.Follow = vCam.LookAt = members[0].transform;
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
