// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System {

    public unsafe struct ByteSpan {
        internal byte* _data;
        internal int _length;
        internal int _id;

        [CLSCompliant(false)]
        public ByteSpan(byte* data, int length, int id = -1)
        {
            _id = id;
            _data = data;
            _length = length;
        }

        public byte this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (index >= Length) Environment.FailFast("index out of range");
                return _data[index];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (index >= Length) Environment.FailFast("index out of range");
                _data[index] = value;
            }
        }

        [CLSCompliant(false)]
        public void Set(byte* value, int valueLength)
        {
            Precondition.Require(valueLength <= Length);
            Buffer.MemoryCopy(value, _data, _length, valueLength);
        }

        [CLSCompliant(false)]
        public unsafe byte* UnsafeBuffer
        {
            get { return _data; }
        }

        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _length;
            }
        }

        public ByteSpan Slice(int index)
        {
            Precondition.Require(index < Length);

            var data = _data + index;
            var length = _length - index;
            return new ByteSpan(data, length, -1);
        }

        public ByteSpan Slice(int index, int count)
        {
            Precondition.Require(index + count < Length);

            var data = _data + index;
            return new ByteSpan(data, count, -1);
        }
    }
}