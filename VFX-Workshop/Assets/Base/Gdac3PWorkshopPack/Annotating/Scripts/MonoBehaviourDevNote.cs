using UnityEngine;

namespace Gdac3PWorkshopPack.Annotating.Scripts
{
    public class MonoBehaviourDevNote : MonoBehaviour
    {
        [SerializeField]
        [TextArea(3, 10)]
        protected string _workshopNote;
    }
}