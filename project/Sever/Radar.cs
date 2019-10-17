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
        //目标搜寻方式
        public enum RadarType
        {
            front_first = 0,    //前排优先
        }


        //前排优先
        public static int[,,] front_first = new int[,,]
             {
              {{0,0},{1,0},{-1,0},{2,0},{-2,0},{3,0},{-3,0}},
              {{0,-1},{1,-1},{-1,-1},{2,-1},{-2,-1},{3,-1},{-3,-1}}
             };



        /// <summary>
        /// 获取目标索引
        /// </summary>
        /// <param name="speedItemList">在场单位列表（OrderBySpeed）</param>
        /// <param name="radarType">搜索方式</param>
        /// <param name="selfCamp">本方阵营</param>
        /// <param name="selfPos">当前单位的位置</param>
        /// <returns></returns>
        public static int GetTargetIndex(List<DataBase.SpeedItemList> speedItemList, RadarType radarType, Camps selfCamp, int selfPos)
        {
            int posAdjust_Y = -1 * DataBase.cardPos[selfPos].y;
            DataBase.Pos targetPos = new DataBase.Pos();


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //设置目标的坐标-前排优先
                    int posX = DataBase.cardPos[selfPos].x + front_first[i, j, 0];
                    int posY = DataBase.cardPos[selfPos].y + front_first[i, j, 1] + posAdjust_Y;
                    targetPos.set(posX, posY);
                    Console.WriteLine("posX: " + posX + "  posY: " + posY);

                    //获取目标索引
                    for (int s = 0; s < speedItemList.Count; s++)
                    {           
                        if (speedItemList[s].card.BornPos == DataBase.GetPosIndex(targetPos))
                            if (speedItemList[s].camp != selfCamp)
                                return s;
                    }

                }
            }
            return -1;
        }
    }
}
