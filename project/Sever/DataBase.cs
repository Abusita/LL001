using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsProtobuf;


namespace Sever
{
    class DataBase
    {

        #region == Data.Pos == 

        public static Dictionary<int, Pos> cardPos = new Dictionary<int, Pos>();

        public static int GetPosIndex(Pos p)
        {
            foreach (var item in cardPos)
            {
                if (item.Value.isEqual(p))
                    return item.Key;
            }
            return -1;
        }

        /// <summary>
        /// 初始化坐标信息
        /// </summary>
        private static void InitPos()
        {
            int[,,] pos = new int[2, 4, 2]
             {
              {{0,0 },{1,0},{2,0},{3,0}},
              {{0,-1},{1,-1},{2,-1},{3,-1}}
             };

            int index = 0;
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 4; j++)
                {
                    Pos p = new Pos();
                    p.set(pos[i, j, 0], pos[i, j, 1]);
                    cardPos.Add(index, p);
                    index++;
                }
        }

        #endregion




        /// <summary>
        /// 坐标
        /// </summary>
        public class Pos
        {
            public int x = 0;
            public int y = 0;

            public void set(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public bool isEqual(Pos p)
            {
                if ((this.x == p.x) && (this.y == p.y))
                    return true;
                return false;
            }
        }


        /// <summary>
        /// 战斗单位列表（order by speed）
        /// </summary>
        public class SpeedItemList
        {
            public Camps camp = Camps.CsCampPlayer;
            public CardMsg card = new CardMsg();

        }

        /// <summary>
        /// 初始化接口
        /// </summary>
        public static void Init()
        {
            InitPos();
        }

    }
}
