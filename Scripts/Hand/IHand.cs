using UnityEngine;

namespace u1w202504
{
    public interface IHand
    {
        Vector2 CurPos { get; }
        void SetAimPos(Vector2 aimPos);
        void SetFree();
        void Hide();
    }
}