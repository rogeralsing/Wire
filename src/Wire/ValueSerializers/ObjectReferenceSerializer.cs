﻿// -----------------------------------------------------------------------
//   <copyright file="ObjectReferenceSerializer.cs" company="Asynkron HB">
//       Copyright (C) 2015-2017 Asynkron HB All rights reserved
//   </copyright>
// -----------------------------------------------------------------------

using System;
using System.Buffers;
using System.IO;
using Wire.Extensions;

namespace Wire.ValueSerializers
{
    public class ObjectReferenceSerializer : ValueSerializer
    {
        public const byte Manifest = 253;
        public static readonly ObjectReferenceSerializer Instance = new ObjectReferenceSerializer();

        public override void WriteManifest(IBufferWriter<byte> stream, SerializerSession session)
        {
            stream.WriteByte(Manifest);
        }

        public override void WriteValue(IBufferWriter<byte> stream, object value, SerializerSession session)
        {
            Int32Serializer.WriteValueImpl(stream, (int) value);
        }

        public override object ReadValue(Stream stream, DeserializerSession session)
        {
            var id = stream.ReadInt32(session);
            var obj = session.GetDeserializedObject(id);
            return obj;
        }

        public override Type GetElementType()
        {
            throw new NotImplementedException();
        }
    }
}