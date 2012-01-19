using System;
using System.Collections;
using System.Text;

namespace MyOS
{
    public class Page
    {
        public const int BLOCK_SIZE = 50;

        private int baseNum, occupied;

        public Page(int baseNum, int occupied)
        {
            this.baseNum = baseNum;
            this.occupied = occupied;
        }

        public int BaseNum
        {
            get
            {
                return baseNum;
            }
        }

        /**Returns the real size occupied by the page.
         * */
        public int Occupied
        {
            get
            {
                return occupied;
            }
        }
    }
}
	