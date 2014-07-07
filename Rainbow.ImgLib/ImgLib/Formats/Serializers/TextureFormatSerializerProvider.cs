﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rainbow.ImgLib.Formats.Serializers
{
    /// <summary>
    /// This class helps creating the right TextureFormatSerializer for a given source texture. The texture can be given in stream form or file form.
    /// Given a file extension, it is also possible to request a serializer of a texture format whose preferred file extension if the given one.
    /// User made serializers can be made available at runtime by means of the static method RegisterSerializer.
    /// </summary>
    public static class TextureFormatSerializerProvider
    {
        private static List<TextureFormatSerializer> serializers = new List<TextureFormatSerializer>();

        static TextureFormatSerializerProvider()
        {
            RegisterSerializer(new TIM2TextureSerializer());
        }

        /// <summary>
        /// The list of all available TextureFormatSerializers
        /// </summary>
        public static IEnumerable<TextureFormatSerializer> RegisteredSerializers
        {
            get
            {
                return serializers;
            }
        }
        /// <summary>
        /// Adds the given serializer to the list of available TextureFormatSerializers.
        /// </summary>
        /// <param name="serializer"></param>
        public static void RegisterSerializer(TextureFormatSerializer serializer)
        {
            serializers.Add(serializer);
        }

        /// <summary>
        /// Retrieves a serializer for textures of the given file format extension.
        /// </summary>
        /// <param name="formatExtension"></param>
        /// <returns>the requested serializer if found, null otherwise.</returns>
        public static TextureFormatSerializer FromFileFormatExtension(string formatExtension)
        {
            foreach (var serializer in serializers)
                if (serializer.PreferredFormatExtension == formatExtension)
                    return serializer;

            return null;
        }

        /// <summary>
        /// Retrieves a serializer for textures of the given metadata file extension
        /// </summary>
        /// <param name="metadataExtension"></param>
        /// <returns>the requested serializer if found, null otherwise.</returns>
        public static TextureFormatSerializer FromFileMetadataExtension(string metadataExtension)
        {
            foreach (var serializer in serializers)
                if (serializer.PreferredMetadataExtension == metadataExtension)
                    return serializer;

            return null;
        }

        /// <summary>
        /// Retrieves a serializer for textures encoded in the same format of the given stream. The given stream can be either a file format stream or a metadata stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>the requested serializer if found, null otherwise.</returns>
        public static TextureFormatSerializer FromStream(Stream stream)
        {
            foreach (var serializer in serializers)
                if (serializer.IsValidFormat(stream) || serializer.IsValidMetadataFormat(stream))
                    return serializer;

            return null;
        }

        /// <summary>
        /// Retrieves a serializer for textures encoded in the same format of the given file. The given file can either encode a texture in metadata or original form.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>the requested serializer if found, null otherwise.</returns>
        public static TextureFormatSerializer FromFile(string filePath)
        {
            using (Stream s = File.Open(filePath, FileMode.Open))
                return FromStream(s);
        }
    }
}
