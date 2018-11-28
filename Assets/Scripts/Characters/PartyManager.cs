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

    [SerializeField]
    GameObject beforeRobot;
    [SerializeField]
    GameObject afterRobot;

    private void Start()
    {
        partyMembers = new Queue<GameObject>();
        int i = 0;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            partyMembers.Enqueue(player);
            //Debug.Log("Added: " + player.name + " PartyNumber: " + i);
            player.GetComponent<Character3D>().partyNumber = i++;
        }
        members = new GameObject[] {new GameObject(), new GameObject(), new GameObject(), new GameObject(), new GameObject()};
        //Debug.Log(partyMembers);
        members = partyMembers.ToArray();
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
            //old load
            beforeRobot.SetActive(partyPos.BeforeRobot);
            afterRobot.SetActive(partyPos.AfterRobot);
            //newLoad

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
            members = partyMembers.ToArray();
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

    public static void DeletePartyMember(GameObject deadMember)
    {
        GameObject[] oldMembers = partyMembers.ToArray();
        int oldSize = oldMembers.Length;
        for(int i = 0; i < oldSize; i++)
        {
            partyMembers.Dequeue();
        }
        int j = 0;
        foreach (GameObject member in oldMembers)
        {
            if(member != deadMember)
            {
                member.GetComponent<Character3D>().partyNumber = j++;
                partyMembers.Enqueue(member);
            }
        }
        members = partyMembers.ToArray();
        if (members.Length > 0)
            vCam.Follow = vCam.LookAt = members[0].transform;
        else {
            MenuController.deadScreen = true;
        }
    }

    IEnumerator WaitToSwap()
    {
        for (; ; )
        {
            canSwap = true;
            yield return new WaitForSeconds(.5f);
        }
    }

}
