using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyOS
{
    public class Memory
    {
        public const int EMPTY = 0;
        public const int FULL = -1;
        public const int OS = -2;

        public const int COMPACTION = 0;
        public const int PAGING = 1;

        private int osBlockSize;
        private int usedMemory;
        private int sizeOfPages;
        private int[] block;

        private LinkedList<Page> pages;

        public Memory(int capacity, int osBlockSize)
        {
            this.osBlockSize = osBlockSize;
            block = new int[capacity];
            usedMemory += osBlockSize;

            for (int i = 0; i < capacity; i++)
            {
                if (i < osBlockSize)
                {
                    block[i] = OS;
                }
                else
                {
                    block[i] = EMPTY;
                }
            }
        }

        /**Fills a block of memory with the ID of the PCB.
         * */
        public int Fill(int ID, int size)
        {
            lock (this)
            {
                for (int i = osBlockSize; i < block.Length; i++)
                {
                    if (i + size >= block.Length)
                    {
                        return FULL;
                    }

                    for (int j = 0; j < size; j++)
                    {
                        if (block[i+j] != EMPTY)
                        {
                            break;
                        }
                        else if (j + 1 == size)
                        {
                            for (j = 0; j < size; j++)
                            {
                                block[i + j] = ID;
                            }
                            return i;
                        }
                    }
                }
                return FULL;
            }
        }

        /**Sets the number of pages in the logical memory available for allocation.
         * */
        public void InitPages()
        {
            int size = Capacity - OSBlockSize;
            int numPages = size / Page.BLOCK_SIZE + (size % Page.BLOCK_SIZE == 0 ? 0 : 1);
            pages = new LinkedList<Page>();
            for (int i = 0, baseNum = osBlockSize; i < numPages; i++, baseNum+=Page.BLOCK_SIZE)
            {
                pages.AddFirst(new Page(baseNum, 50));
            }
        }

        /**Allocates a corresponding number of pages to a process 
         * relative to its size by removing it from the Free Page List (pages).
         * */
        public ArrayList CreatePages(int size, int request, bool isDemand)
        {
            ArrayList pcbPages = new ArrayList();
            sizeOfPages = 0;
            lock (this.pages)
            {
                if (request <= pages.Count)
                {
                    for (int i = 0; i < request; i++)
                    {
                        if (i + 1 == request)
                        {
                            pcbPages.Add(new Page(pages.Last.Value.BaseNum, size % Page.BLOCK_SIZE));
                            sizeOfPages += size % Page.BLOCK_SIZE;
                        }
                        else
                        {
                            pcbPages.Add(pages.Last.Value);
                            sizeOfPages += Page.BLOCK_SIZE;
                        }
                        pages.RemoveLast();
                    }
                    return pcbPages;
                }
                else if (isDemand && pages.Count > 0)
                {
                    pcbPages.Add(new Page(pages.Last.Value.BaseNum, size % Page.BLOCK_SIZE));
                    pages.RemoveLast();
                    sizeOfPages = size % Page.BLOCK_SIZE;
                    while (pages.Count > 0)
                    {
                        pcbPages.Add(pages.Last.Value);
                        pages.RemoveLast();
                        sizeOfPages += Page.BLOCK_SIZE;
                    }
                    return pcbPages;
                }
                else
                {
                    return null;
                }
            }
        }

        public ArrayList CreatePages(int needed)
        {
            sizeOfPages = 0;
            ArrayList pcbPages = new ArrayList();
            lock (this.pages)
            {
                while (pages.Count > 0 && pcbPages.Count < needed)
                {
                    pcbPages.Add(pages.Last.Value);
                    pages.RemoveLast();
                    sizeOfPages += Page.BLOCK_SIZE;
                }
                return pcbPages;
            }
        }

        public int SizeOfPages
        {
            get
            {
                return sizeOfPages;
            }
        }

        /**Clears a block of memory starting from the parameter supplied
         * up to its end.
         * */
        public void Clear(int begin)
        {
            lock (this)
            {
                for (int i = osBlockSize; i < block.Length; i++)
                {
                    block[i] = EMPTY;
                }
            }
        }

        /**Liberates a sizeable block of memory starting from the supplied base number.
         * */
        public void Free(int baseNum, int size)
        {
            lock (this)
            {
                for (int i = baseNum; i < baseNum + size; i++)
                {
                    block[i] = EMPTY;
                }
            }
        }

        /**Liberates a number of pages and restores them to the Free Page List (pages).
         * */
        public void Free(ArrayList pages)
        {
            lock (this)//lock this.pages
            {
                for (int i = 0; i < pages.Count; i++)
                {
                    Page page = (Page)pages[i];
                    page = new Page(page.BaseNum, Page.BLOCK_SIZE);
                    this.pages.AddLast(page);
                }
            }
        }

        public int FreePages
        {
            get
            {
                return pages.Count;
            }
        }

        public int FreeMemory
        {
            get
            {
                return Capacity - UsedMemory;
            }
        }

        public int UsedMemory
        {
            set
            {
                usedMemory = value;
            }
            get
            {
                return usedMemory;
            }
        }


        public int OSBlockSize
        {
            get
            {
                return osBlockSize;
            }
        }

        public int Capacity
        {
            get
            {
                return block.Length;
            }
        }

        public int GetOccupant(int index)
        {
            return block[index];
        }
    }
}
