using System;
using System.Collections;
using System.IO;
using System.Numerics;

namespace DataStructures.Hashes.BloomFilterDS
{
    public class BloomFilter
    {
        private HashFunctionArray hashesArray;
        private BitArray bitArray;
        private int bitArrayLength;
        BigInteger bigIntArrayLength;

        public BloomFilter(HashFunctionArray hashesArray, int length)
        {
            if (hashesArray == null)
                throw new ArgumentNullException("hashesArray cannot be null");
            if (length < 1)
                throw new ArgumentOutOfRangeException("length of bitArray must be greater than 0");

            this.hashesArray = hashesArray;
            bitArray = new BitArray(length);
            bitArrayLength = length;
            bigIntArrayLength = new BigInteger(length);
        }

        public void Add(Stream stream)
        {
            HashFunctionArray.HashOutput[] hashes = hashesArray.GetHashes(stream);
            SetBits(hashes);
        }

        public void Add(byte[] data)
        {
            HashFunctionArray.HashOutput[] hashes = hashesArray.GetHashes(data);
            SetBits(hashes);
        }

        public void Add(byte[] data, int offset, int count)
        {
            HashFunctionArray.HashOutput[] hashes = hashesArray.GetHashes(data, offset, count);
            SetBits(hashes);
        }

        public bool Query(byte[] data)
        {
            return Query(data, 0, data.Length);
        }

        public bool Query(Stream stream)
        {
            HashFunctionArray.HashOutput[] hashes = hashesArray.GetHashes(stream);

            return Contains(hashes);
        }

        public bool Query(byte[] data, int offset, int count)
        {
            HashFunctionArray.HashOutput[] hashes = hashesArray.GetHashes(data);

            return Contains(hashes);
        }

        private bool Contains(HashFunctionArray.HashOutput[] hashes)
        {
            int index = -1;
            for (int i = 0; i < hashes.Length; i++)
            {
                index = GetBitIndex(hashes[i]);

                if (!bitArray.Get(index))
                    return false;
            }

            return true;
        }

        private void SetBits(HashFunctionArray.HashOutput[] output)
        {
            int index = -1;
            for (int i = 0; i < output.Length; i++)
            {
                index = GetBitIndex(output[i]);
                bitArray.Set(index, true);
            }
        }

        private int GetBitIndex(HashFunctionArray.HashOutput hashResult)
        {
            BigInteger hash = new BigInteger(hashResult.Hash);
            if (hash.Sign < 0)
                hash *= -1;

            int index = (int)(hash % bigIntArrayLength);

            return index;
        }
    }
}
