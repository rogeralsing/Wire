﻿// -----------------------------------------------------------------------
//   <copyright file="FromSurrogateSerializer.cs" company="Asynkron HB">
//       Copyright (C) 2015-2017 Asynkron HB All rights reserved
//   </copyright>
// -----------------------------------------------------------------------

using System;
using System.Buffers;
using System.IO;

namespace Wire.ValueSerializers
{
    public class FromSurrogateSerializer : ValueSerializer
    {
        private readonly ValueSerializer _surrogateSerializer;
        private readonly Func<object, object> _translator;

        public FromSurrogateSerializer(Func<object, object> translator, ValueSerializer surrogateSerializer)
        {
            _translator = translator;
            _surrogateSerializer = surrogateSerializer;
        }

        public override void WriteManifest(IBufferWriter<byte> stream, SerializerSession session)
        {
            throw new NotSupportedException();
        }

        public override void WriteValue(IBufferWriter<byte> stream, object value, SerializerSession session)
        {
            throw new NotSupportedException();
        }

        public override object ReadValue(Stream stream, DeserializerSession session)
        {
            var surrogateValue = _surrogateSerializer.ReadValue(stream, session);
            var value = _translator(surrogateValue);
            return value;
        }

        public override Type GetElementType()
        {
            throw new NotImplementedException();
        }
    }
}