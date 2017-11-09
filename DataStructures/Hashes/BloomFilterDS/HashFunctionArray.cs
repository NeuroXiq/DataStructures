using System;
using System.IO;
using System.Security.Cryptography;

namespace DataStructures.Hashes.BloomFilterDS
{
    public class HashFunctionArray
    {
        public struct HashOutput
        {
            ///<summary>Length in bist of Hash</summary>
            public int Length;
            public byte[] Hash;

            public HashOutput(byte[] hash, int length)
            {
                Hash = hash;
                Length = length;
            }
        }

        ///<summary>Number of stored hash functions</summary>
        public int  Count { get { return hashFunctions.Length; } }

        private readonly HashAlgorithm[] hashFunctions;

        public HashFunctionArray(HashAlgorithm[] hashFunctions)
        {
            ThrowIfHashAlgorithmsInvalid(hashFunctions);

            this.hashFunctions = hashFunctions;
        }

        public HashOutput[] GetHashes(Stream stream)
        {
            HashOutput[] output = new HashOutput[Count];
            HashAlgorithm current;
            for (int i = 0; i < Count; i++)
            {
                current = hashFunctions[i];

                byte[] hash = current.ComputeHash(stream);
                output[i] = new HashOutput(hash, current.OutputBlockSize);
            }

            return output;
        }

        public HashOutput[] GetHashes(byte[] buffer, int offset, int count)
        {
            HashOutput[] output = new HashOutput[Count];
            HashAlgorithm current;
            for (int i = 0; i < Count; i++)
            {
                current = hashFunctions[i];

                byte[] hash = current.ComputeHash(buffer, offset, count);
                output[i] = new HashOutput(hash, current.OutputBlockSize);
            }

            return output;
        }

        public HashOutput[] GetHashes(byte[] buffer)
        {
            return GetHashes(buffer, 0, buffer.Length);
        }

        private void ThrowIfHashAlgorithmsInvalid(HashAlgorithm[] hashes)
        {
            if (hashes == null)
                throw new ArgumentNullException("hashFunctions array cannot be null");
            if (hashes.Length == 0)
                throw new ArgumentException("hashFunctions array must contains at least 1 hash function");
            else
            {
                for (int i = 0; i < hashes.Length; i++)
                {
                    if (hashes[i] == null)
                    {
                        string error = string.Format("Hash function in hashFunctions array at {0} index is null.", i);
                        throw new ArgumentNullException(error);
                    }
                }
            }
        }

    }
}
