using NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Interact : MonoBehaviour
    {
        [SerializeField] GUIStyle _crossHairStyle;
        [SerializeField] LayerMask _layerMask;//This is for the interaction
        [SerializeField] bool _showToolTip;
        public string action, button, instructions;
        // Update is called once per frame
        void Update()
        {
            // create a line
            Ray interactionRay;
            // set our origin and direction
            interactionRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
            // store data from collision
            RaycastHit hitInfo;

            if (Physics.Raycast(interactionRay, out hitInfo, 10,_layerMask))
            {
                Debug.DrawRay(interactionRay.origin, transform.forward * 10,Color.green);
                _showToolTip = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    #region NO GROSS PLZ STOP
                    //if (hitInfo.collider.tag == "NPC")
                    //{
                    //    if (hitInfo.collider.GetComponent<LinearIMGUIDlg>())
                    //    {
                    //        hitInfo.collider.GetComponent<LinearIMGUIDlg>().showDlg = true;
                    //    }
                        //if (hitInfo.collider.GetComponent<LinearIMGUIDlgWithChoice>())
                        //{

                        //}
                        //if (hitInfo.collider.GetComponent<LinearIMGUIDlgWithFriendship>())
                        //{

                        //}
                        //if (hitInfo.collider.GetComponent<LinearIMGUIDlgWithShop>())
                        //{

                        //}
                        //if (hitInfo.collider.GetComponent<LinearIMGUIDlgWithQuest>())
                        //{

                        //}
                    //}
                    //if (hitInfo.collider.CompareTag("Chest"))
                    //{
                    //    //check if script in on chest
                    //    //if it is activate
                    //}
                    //if (hitInfo.collider.tag == "Item")
                    //{
                    //    //check if script in on Item
                    //    //if it is activate
                    //}
                    //if (hitInfo.collider.CompareTag("Bed"))
                    //{

                    //}
                    //if (hitInfo.collider.CompareTag("Campfire"))
                    //{

                    //}
                    //if (hitInfo.collider.CompareTag("CraftingStation"))
                    //{

                    //}
                    //if (hitInfo.collider.CompareTag("Door"))
                    //{

                    //}
                    //if (hitInfo.collider.CompareTag("Shop"))
                    //{

                    //}
                    #endregion
                    #region YASSSSSSSSSS!!!!
                    if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interact))
                    {
                        interact.OnInteraction();
                    }
                    #endregion
                }
            }
        }
    }
}

