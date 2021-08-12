using Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public class DisplayStatusState : MonoBehaviour
    {
        [SerializeField] private Image icon;

        [Header("Sprites")]
        [SerializeField] private Sprite idleStatus;

        [SerializeField] private Sprite runStatus;
        [SerializeField] private Sprite openDoorStatus;

        public void Display(StateType stateType)
        {
            switch (stateType)
            {
                case StateType.Idle:
                    icon.sprite = idleStatus;
                    break;
                case StateType.OpenDoor:
                case StateType.MoveToRoom:
                    icon.sprite = openDoorStatus;
                    break;
                case StateType.Walk:
                    icon.sprite = runStatus;
                    break;
                default:
                    icon.sprite = idleStatus;
                    break;
            }
        }
    }
}