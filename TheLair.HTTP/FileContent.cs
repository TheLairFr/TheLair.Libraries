﻿namespace TheLair.HTTP
{
    public class FileContent
    {
        public Stream Stream;
        public string Name = "File";
        public string? Parameter;
        public long Size;

        public FileContent(Stream stream, string name, string parameter, long size)
        {
            Stream = stream;
            Name = name;
            Parameter = parameter;
            Size = size;
        }

        public FileContent(Stream stream, string name, long size)
        {
            Stream = stream;
            Name = name;
            Size = size;
        }

        public FileContent(Stream stream, long size)
        {
            Stream = stream;
            Size = size;
        }
    }
}
