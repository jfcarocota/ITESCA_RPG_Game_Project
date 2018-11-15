using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCore.SystemControls;

namespace GameCore {
    namespace TalkSystem {
        public class TalkSystem : MonoBehaviour {

            [SerializeField]
            GameObject dialogBox;
            [SerializeField]
            Text displayText;
            [SerializeField]
            Image displayImage;
            [SerializeField]
            Sprite[] characterImages;
            
            List<string> dialogs;
            string[] splitDialog;
            int currentDialogPos;
            
            void Start() {
                dialogs = new List<string>();
                dialogs.Add("00Hey muy buenas a todos|00Aqui Willyrex comentando|00Y hoy os traigo un nuevo gueimplei del maincra");
            }

            private void Update() {
                if (dialogBox.activeSelf && Controllers.GetFire(1, 2)) {
                    AdvanceDialog();
                }
            }

            public void ShowDialog(int index) {
                //aqui ponerle la pausa al juego
                Time.timeScale = 0;

                dialogBox.SetActive(true);
                splitDialog = dialogs[index].Split('|');
                currentDialogPos = 0;
                displayImage.sprite = characterImages[int.Parse(splitDialog[currentDialogPos].Substring(0, 2))];
                displayText.text = splitDialog[currentDialogPos++].Substring(2);
            }

            void AdvanceDialog() {
                if( currentDialogPos < splitDialog.Length) {
                    displayImage.sprite = characterImages[int.Parse(splitDialog[currentDialogPos].Substring(0, 2))];
                    displayText.text = splitDialog[currentDialogPos++].Substring(2);
                }
                else {
                    CloseDialog();
                }
            }

            void CloseDialog() {
                //aqui quitarle la pausa al juego
                Time.timeScale = 1;

                dialogBox.SetActive(false);
            }

            private void OnTriggerEnter(Collider other) {
                ShowDialog(0);
            }

        }
    }
}
