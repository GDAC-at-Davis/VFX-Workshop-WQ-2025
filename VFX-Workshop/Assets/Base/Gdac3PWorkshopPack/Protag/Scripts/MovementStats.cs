using Gdac3PWorkshopPack.Annotating.Scripts;

namespace Gdac3PWorkshopPack.Protag.Scripts
{
    /// <summary>
    ///     Stat variable holder for protag movement
    /// </summary>
    public class MovementStats : MonoBehaviourDevNote
    {
        public float MoveSpeed;

        public float MoveAccel;

        public float MoveFriction;

        public float JumpVelocity;

        private void OnValidate()
        {
            _workshopNote = @"Feel free to modify the player's variables!";
        }
    }
}