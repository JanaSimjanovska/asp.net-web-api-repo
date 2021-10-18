using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Domain.Models
{
    public class WinningCombination : BaseEntity
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Number7 { get; set; }
        public int Number8 { get; set; }
        public Session Session { get; set; }
        public int SessionId { get; set; }

        public List<int> WinningComboList { get; set; } = new List<int>();

        // NE RABOTI, RANDOMOT SEKOJ PAT JA MENJA VREDNOSTA PRI MAPIRANJE, RAZMISLI
        public List<int> GetWinningCombination(List<int> winningCombo)
        {
            Random randomNum = new Random();
            int num = 0;
            //List<int> randomList = new List<int>();

            while (winningCombo.Count != 8)
            {
                num = randomNum.Next(1, 38);
                if (winningCombo.Contains(num))
                    continue;
                winningCombo.Add(num);
            }

            return winningCombo;
        }

        public void SetNumbers()
        {
            Number1 = WinningComboList[0];
            Number2 = WinningComboList[1];
            Number3 = WinningComboList[2];
            Number4 = WinningComboList[3];
            Number5 = WinningComboList[4];
            Number6 = WinningComboList[5];
            Number7 = WinningComboList[6];
            Number8 = WinningComboList[7];
        }
    }
}
