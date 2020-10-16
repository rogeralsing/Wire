﻿// -----------------------------------------------------------------------
//   <copyright file="CharSerializer.cs" company="Asynkron HB">
//       Copyright (C) 2015-2017 Asynkron HB All rights reserved
//   </copyright>
// -----------------------------------------------------------------------

using System;
using System.Buffers;
using System.IO;
using Wire.Internal;

namespace Wire.ValueSerializers
{
    public class CharSerializer : SessionAwareValueSerializer<char>
    {
        public const byte Manifest = 15;
        private const int Size = sizeof(char);
        public static readonly CharSerializer Instance = new CharSerializer();

        private CharSerializer() : base(Manifest, () => WriteValueImpl, () => ReadValueImpl)
        {
        }

        public override int PreallocatedByteBufferSize => Size;

        private static char ReadValueImpl(Stream stream, byte[] bytes)
        {
            stream.Read(bytes, 0, Size);
            return BitConverter.ToChar(bytes, 0);
        }

        private static void WriteValueImpl(IBufferWriter<byte> stream, char ch, int size)
        {
            var span = stream.GetSpan(size);
            BitConverter.TryWriteBytes(span,ch);
            stream.Advance(size);
        }
    }
}