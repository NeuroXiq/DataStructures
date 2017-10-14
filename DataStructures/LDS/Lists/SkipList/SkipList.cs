using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LDS.Lists
{
    ///<summary>Generic list implementation of skip list data structure</summary>
    public class SkipList<T>
    {
        private Comparison<T> keyCompare;
        private SLNode<T> head;
        private Random random;
        private readonly double pFactor;

        ///<summary> Initialize new instance of <see cref="SkipList{T}"/></summary>
        ///<param name="keyCompare"><see cref="Comparison{T}"/> delegate indicating comparison between two <see cref="{T}"/> objects</param>
        public SkipList(Comparison<T> keyCompare) : this(keyCompare, 2)
        {
            bool x = head.NextExist(0);
        }

        ///<summary>Initialize new instance of <see cref="SkipList{T}"/></summary>
        ///<param name="keyCompare"><see cref="Comparison{T}"/> callback indication comparison between two <see cref="{T}"/> object</param>
        ///<param name="pFactor">Parameter specifying probability of level height in new node</param>
        ///<example>
        ///pFactor is used as probability factor in randomization of level height in new node.
        ///Probability: p = 1/pFactor.
        ///pFactor cannot be smaller or equal to 1, becouse new height will grow to inifinity.
        ///Count of nodes on level 'L' in list with 'N' added nodes
        ///probably will be: N*(p^L)
        ///where p=1/pFactor.
        ///For bigger pFactor, levels will grow slowly and vice versa.
        /// </example>
        public SkipList(Comparison<T> keyCompare, double pFactor)
        {
            this.keyCompare = keyCompare;
            this.random = new Random(Environment.TickCount);
            this.head = new SLNode<T>(default(T), new SLNode<T>[4]);
            this.pFactor = pFactor;
        }

        ///<summary>Insert new key into list</summary>
        public void Add(T key)
        {
            int newNodeLevel = GetRandomLevel();
            if (newNodeLevel > head.Level)
                ExtendHeadLevel(newNodeLevel);

            SLNode<T>[] update = GetAddHelpData(key);
            SLNode<T> newNode = new SLNode<T>(key, new SLNode<T>[newNodeLevel]);
            
            for (int i = newNodeLevel - 1; i >= 0; i--)
            {
                newNode.SetNext(i, update[i].Next[i]);
                update[i].SetNext(i, newNode);
            }
        }

        public void Show()
        {
            for (int i = 0; i < head.Level; i++)
            {
                SLNode<T> node = head.Next[i];
                while (true)
                {
                    if (node == null)
                    {
                        Console.Write("nul");
                        break;
                    }
                    else
                    {
                        Console.Write(node.Key.ToString() + "->");
                    }
                    node = node.Next[i];
                }


                Console.WriteLine();
            }
        }

        ///<summary>Delete key from list</summary>
        public void Delete(T key)
        {
            //getaddhelpdata can be also used to find where node is
            SLNode<T>[] trail = GetAddHelpData(key);

            if (trail[0].NextExist(0))
            {
                if(keyCompare(trail[0].GetNextKey(0),key) == 0)
                {
                    SLNode<T> toDelete = trail[0].Next[0];
                    for (int i = toDelete.Level - 1; i >= 0; i--)
                    {
                        trail[i].SetNext(i, toDelete.Next[i]);
                    }
                }
            }

        }

        ///<summary>Checks if list contains key</summary>
        ///<param name="key">Key to check</param>
        ///<returns>Returns true if <see cref="SkipList{T}"/> contains key otherwise return false</returns>
        public bool Contains(T key)
        {
            SLNode<T> current = head;
            for (int h = head.Level - 1; h >= 0; h--)
            {
                while(current.NextExist(h))
                {
                    int cResult = keyCompare(key, current.GetNextKey(h));
                    //Console.Write(key + "-" + current.GetNextKey(h) + ":");
                    if (cResult == 0)
                        return true;
                    else if (cResult > 0)
                    {
                        current = current.Next[h];
                    }
                    else break;
                }
               // Console.WriteLine();
            }

            return false;
        }

        private int GetRandomLevel()
        {
            //level must be greater than 0
            int randomLevel = 0;
            int randValue = int.MaxValue;
            int randomMax = Int32.MaxValue;

            do
            {
                randomLevel++;
                randValue = random.Next(randomMax);

            } while (randValue < (randomMax/this.pFactor));

            return randomLevel;
        }

        private SLNode<T>[] GetAddHelpData(T key)
        {
            SLNode<T>[] update = new SLNode<T>[head.Level];
            SLNode<T> current = head;

            for (int i = head.Level - 1; i >= 0; i--)
            {
                while (current.NextExist(i))
                {
                    if (keyCompare(key, current.GetNextKey(i)) > 0)
                        current = current.Next[i];
                    else
                    {
                        break;
                    }
                }
                update[i] = current;
            }

            return update;
        }

        private void ExtendHeadLevel(int newSize)
        {
            SLNode<T>[] newNodes = new SLNode<T>[newSize];
            for (int i = 0; i < head.Level; i++)
            {
                newNodes[i] = head.Next[i];
            }

            head = new SLNode<T>(head.Key, newNodes);
        }
    }
}
