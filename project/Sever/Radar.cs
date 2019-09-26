using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsProtobuf;


namespace Sever
{
    class Radar
    {
        public enum RadarType
        {
            front_first = 0,
        }


        //前排优先
        public static int[,,] front_first = new int[,,]
             {
              {{0,0},{1,0},{-1,0},{2,0},{-2,0},{3,0},{-3,0}},
              {{0,-1},{1,-1},{-1,-1},{2,-1},{-2,-1},{3,-1},{-3,-1}}
             };




        public static int GetTargetIndex(List<Data.SpeedItemList> speedItemList, RadarType radarType, Camps selfCamp, int selfPos)
        {
            int posAdjust_Y = -1 * Data.cardPos[selfPos].y;
            Data.Pos targetPos = new Data.Pos();


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    int posX = Data.cardPos[selfPos].x + front_first[i, j, 0];
                    int posY = Data.cardPos[selfPos].y + front_first[i, j, 1] + posAdjust_Y;
                    targetPos.set(posX, posY);
                    Console.WriteLine("posX: " + posX + "  posY: " + posY);

                    for (int s = 0; s < speedItemList.Count; s++)
                    {           
                        if (speedItemList[s].card.BornPos == Data.GetPosIndex(targetPos))
                            if (speedItemList[s].camp != selfCamp)
                                return s;
                    }

                }
            }
            return -1;
        }
    }
}
