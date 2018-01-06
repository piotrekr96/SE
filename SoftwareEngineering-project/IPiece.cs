using System.Drawing;

namespace SoftwareEngineering_project
{
    public interface IPiece
    {
        Bitmap getBitmap();
        Bitmap getBmpNonGoal();
        Player getOwner();
        int getPosX();
        int getPosY();
        bool getSham();
        bool getSpent();
        bool placePieceInit(int x, int y);
        void setOwner(Player pl);
        void setPosX(int x);
        void setPosY(int y);
        void setSham(bool s);
        void setSpent();
    }
}