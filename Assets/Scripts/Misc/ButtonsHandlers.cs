using UnityEngine;
using UnityEngine.SceneManagement;

namespace Misc
{
    public class ButtonsHandlers : MonoBehaviour
    {
        private const string TwoRooms = "2 Rooms";
        private const string FourRooms = "4 Rooms";

        public void OnButtonLoad2Rooms() =>
            SceneManager.LoadScene(TwoRooms);

        public void OnButtonLoad4Rooms() =>
            SceneManager.LoadScene(FourRooms);

        public void OnButtonExit() =>
            Application.Quit();
    }
}