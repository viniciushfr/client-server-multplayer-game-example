using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Arena
    {
        private Player[,] arena;
        private int arenaSize;

        public int ArenaSize { get => arenaSize; set => arenaSize = value; }

        public Arena()
        {
            arenaSize = 10;
            arena = new Player[arenaSize, arenaSize];
        }
        public void newPlayer(Player newPlayer)
        {
            Boolean playerAdded = false;
            for(int i = 0; i < arenaSize; i++)
            {
                for(int j = 0; j < arenaSize; j++)
                {
                    if (arena[i, j] == null && !playerAdded)
                    {
                        newPlayer.Y = i;
                        newPlayer.X = j;
                        arena[i, j] = newPlayer;
                        playerAdded = true;
                        break;
                    }
                }
            }
        }

        public Player getPlayer(int x ,int y)
        {
            return arena[x, y];
        }

        public int MovPlayer(Player p,char cmd)
        {
            if (cmd == 'w')
            {
                if (p.Y - 1 >= 0 && arena[p.Y-1,p.X] == null)
                {

                    arena[p.Y-1, p.X] = arena[p.Y, p.X];
                    arena[p.Y,p.X] = null;
                    arena[p.Y-1, p.X].Y -= 1;
                    return 0;
                }
                else
                {
                    return -1;
                }
            }else if(cmd == 's')
            {
                if (p.Y + 1 <= this.arenaSize - 1 && arena[p.Y+1, p.X] == null )
                {
                    arena[p.Y+1, p.X] = arena[p.Y, p.X];
                    arena[p.Y, p.X] = null;
                    arena[p.Y+1, p.X].Y += 1;
                    return 0;
                }
                else
                {
                    return -1;
                }
            }else if(cmd == 'd')
            {
                if (p.X + 1 <= this.arenaSize - 1 && arena[p.Y, p.X+1] == null )
                {
                    arena[p.Y, p.X+1] = arena[p.Y, p.X];
                    arena[p.Y, p.X] = null;
                    arena[p.Y, p.X+1].X += 1;
                    return 0;
                }
                else
                {
                    return -1;
                }
            }else if(cmd == 'a')
            {
                if (p.X - 1 >= 0 && arena[p.Y, p.X-1] == null)
                {
                    arena[p.Y, p.X-1] = arena[p.Y, p.X];
                    arena[p.Y, p.X] = null;
                    arena[p.Y, p.X-1].X -= 1;
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public Player[,] getArena()
        {
            return this.arena;
        }
    }
}
