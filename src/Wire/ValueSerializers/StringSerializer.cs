﻿// -----------------------------------------------------------------------
//   <copyright file="StringSerializer.cs" company="Asynkron HB">
//       Copyright (C) 2015-2017 Asynkron HB All rights reserved
//   </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using Wire.Extensions;
using Wire.Internal;

namespace Wire.ValueSerializers
{
    public class StringSerializer : ValueSerializer
    {
        public const byte Manifest = 7;
        public static readonly StringSerializer Instance = new StringSerializer();

        public static void WriteValueImpl(Stream stream, string s, SerializerSession session)
        {
            var bytes = NoAllocBitConverter.GetBytes(s, session, out var byteCount);
            stream.Write(bytes, 0, byteCount);
        }

        private static string ReadValueImpl(Stream stream, DeserializerSession session) => stream.ReadString(session!)!;

        public override void WriteManifest(Stream stream, SerializerSession session) => stream.WriteByte(Manifest);

        public override void WriteValue(Stream stream, object value, SerializerSession session) => WriteValueImpl(stream, (string) value, session);

        public override object ReadValue(Stream stream, DeserializerSession session) => ReadValueImpl(stream, session);

        public override Type GetElementType() => typeof(string);
    }
}