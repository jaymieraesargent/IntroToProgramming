using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace NPC
{
    public class LinearIMGUIDlg : MonoBehaviour, IInteractable
    {
        public string[] linesOfDlg;
        public int lineIndex;
        public string characterName;
        public bool showDlg, showGrid;
        private void OnGUI()
        {
            if (showGrid)
            {
                Grid();
            }

            Dlg();

        }
        public void OnInteraction()
        {
            showDlg = true;
            GameManager.instance.ChangeGameState(GameState.Menu);
        }
        void Dlg()
        {
            if (showDlg)
            {
                // GUI.Box(UIPos(0, 6, 16, 3),characterName + ": " + linesOfDlg[lineIndex]);
                GUI.Box(UIPos(0, 6, 16, 3), $"{characterName}: {linesOfDlg[lineIndex]}");
                //if we are not on the last line/end of the dlg
                if (lineIndex < linesOfDlg.Length - 1)
                {                       //place this
                    if (GUI.Button(UIPos(14.25f, 6.25f, 1.5f, .5f), "Next"))
                    {
                        //move to next line
                        lineIndex++;
                    }
                }
                else
                {
                    if (GUI.Button(UIPos(14.25f, 6.25f, 1.5f, .5f), "Bye!"))
                    {
                        showDlg = false;
                        lineIndex = 0;
                        GameManager.instance.ChangeGameState(GameState.Game);

                    }
                }
            }
        }
        void Grid()
        {
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    GUI.Box(UIPos(x, y, 1, 1), "");
                    GUI.Label(UIPos(x, y, 1, 1), x + ":" + y);
                }
            }
        }

        private Rect UIPos(float startX, float startY, float sizeX, float sizeY)
        {
            return new Rect(startX * Screen.width / 16, startY * Screen.height / 9, sizeX * Screen.width / 16, sizeY * Screen.height / 9);
        }
    }
}

