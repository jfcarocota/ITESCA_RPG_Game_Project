using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCore.SystemControls;

namespace GameCore {
    namespace TalkSystem {
        public class TalkSystem : MonoBehaviour {

            #region Singleton
            public static TalkSystem Instance;
            private void Awake() {
                Instance = this;
            }
            #endregion

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

            bool win;

            DeathSound deathSound;

            void Start() {
                deathSound = DeathSound.Instance;
                win = false;
                dialogs = new List<string>();
                dialogs.Add("02El Brayan Daniel:\nTa cabrón que el único ****** baño del pueblo este atrás de tu casa >:(|"
                    + "02El Brayan Daniel:\nApesta bien ****** pero no puedo lavarlo por que hay un esqueleto cerca del pozo :(");
                dialogs.Add("03Chin Wong Tong:\n¡HAAAAAAY MANZANAS! ¡SEÑORA, SEÑOR, LA MANZANA! ¡LA MÁS GRANDE, LA MAS JUGOSA! ¡LA MÁS DULCE, SABROSA, RIQUISIMA LA MANZANA!|"
                    + "03Chin Wong Tong:\n¡Oigan amigos! ¡Oigan amigos! ¡¿No quieren unas naranjas?!|" 
                    + "03Chin Wong Tong:\n¡Son tan buenas que acabamos de enviar un carrito al pueblo vecino!");
                dialogs.Add("01Regina Stevens:\n¡Aventureros :o! No se ven muchos por estos rumbos. En especial una party tan variada *smirk*|"
                    + "01Regina Stevens:\nDebido a eso el bosque cercano se encuentra infestado de monstruos :(");
                dialogs.Add("00Guillermo Padrés:\n¡Oh, aventureros! ¡El H. Ayuntamiento del Pueblo les da las gracias por matar a ese esqueleto!|"
                    + "00Guillermo Padrés:\n¡Tres palmadas de recompensa! *Clap, clap, clap*");
                dialogs.Add("04Pancho Pérez:\n¡Ay caramba D:! ¡Aventureros, no vayan para allá!|"
                    + "04Pancho Pérez:\n¡Hay una máquina gigante destruyendo todo!|"
                    + "04Pancho Pérez:\n!Me dió tanto miedo que me voy cagando al pueblo!");
                dialogs.Add("04Pancho Pérez:\n¿Qué? ¿Mataron a todos los monstruos?|"
                    + "04Pancho Pérez:\n¡Oh, vaya! ¡Gracias por salvarme :'D!");
            }

            private void Update() {
                if (dialogBox.activeSelf && Controllers.GetFire(1, 2)) {
                    AdvanceDialog();
                }
            }

            public void ShowDialog(int index) {
                MenuController.isPaused = true;
                Time.timeScale = 0;

                dialogBox.SetActive(true);
                splitDialog = dialogs[index].Split('|');
                currentDialogPos = 0;
                displayImage.sprite = characterImages[int.Parse(splitDialog[currentDialogPos].Substring(0, 2))];
                displayText.text = splitDialog[currentDialogPos++].Substring(2);

                if (index == 5) win = true;
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
                dialogBox.SetActive(false);
                if (win) {
                    deathSound.PlayVictory(PartyManager.members[0].transform.position);
                    MenuController.gameCleared = true;
                    MenuController.deadScreen = true;
                }
                else {
                    MenuController.isPaused = false;
                }
                Time.timeScale = 1f;
            }

        }
    }
}
