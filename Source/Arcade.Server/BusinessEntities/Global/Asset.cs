using System;
using System.IO;

namespace BusinessEntities
{
    public abstract class Asset
    {
        public string Path { get; set; }
        public byte[] Contents { get; set; }
        public virtual string ContentType { get; }


        public Asset(byte[] content)
        {
            SetContent(content);
        }

        public Asset(byte[] content, string path): this(content)
        {
            SetContent(content);
            Path = path;
        }


        private void SetContent(byte[] content)
        {
            this.Contents = ProcessContent(content);
        }

        protected virtual byte[] ProcessContent(byte[] content)
        {
            return content;
        }
    }
}
