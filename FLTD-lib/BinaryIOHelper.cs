using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLTD_lib
{
    internal class BinaryIOHelper : IDisposable
    {
        private FileStream fs;
        private long skip;
        private bool littleEdian;
        public BinaryIOHelper(FileStream fs, bool b)
        {
            this.fs = fs;
            littleEdian = b;
            skip = 0;
        }
        public long Seek(int offset, SeekOrigin origin)
        {
            return fs.Seek(offset, origin);
        }

        public long SkipSeek(int offset)
        {
            return fs.Seek(offset + skip, 0);
        }
        public byte ReadUInt8()
        {
            return (byte)fs.ReadByte();
        }
        public uint ReadUInt32()
        {
            byte[] buf = new byte[sizeof(uint)];
            fs.Read(buf, 0, sizeof(uint));
            if (littleEdian)
                Array.Reverse(buf);
            return BitConverter.ToUInt32(buf, 0);
        }
        unsafe public float ReadFloat()
        {
            byte[] buf = new byte[sizeof(float)];
            fs.Read(buf, 0, sizeof(float));
            if (littleEdian)
                Array.Reverse(buf);
            float f;
            ((byte*)&f)[0] = buf[0];
            ((byte*)&f)[1] = buf[1];
            ((byte*)&f)[2] = buf[2];
            ((byte*)&f)[3] = buf[3];
            return f;
        }
        public string ReadAscii()
        {
            string str = "";
            int c;
            while ((c = fs.ReadByte()) != -1)
            {
                if (c != 0)
                    str += (char)c;
                else
                    return str;
            }
            return str;
        }
        public void WriteUInt8(byte b)
        {
            fs.WriteByte(b);
        }
        public void WriteUInt32(uint i)
        {
            byte[] buf = BitConverter.GetBytes(i);
            if (littleEdian)
                Array.Reverse(buf);
            fs.Write(buf, 0, sizeof(uint));
        }
        unsafe public void WriteFloat(float f)
        {
            byte[] buf = new byte[sizeof(float)];

            buf[0] = ((byte*)&f)[0];
            buf[1] = ((byte*)&f)[1];
            buf[2] = ((byte*)&f)[2];
            buf[3] = ((byte*)&f)[3];

            if (littleEdian)
                Array.Reverse(buf);
            fs.Write(buf, 0, sizeof(float));
        }
        public void WriteAscii(string str)
        {
            foreach (byte b in str)
            {
                fs.WriteByte(b);
            }
        }
        public long Tell() => fs.Position;
        public void SetSkip(long offset)
        {
            skip = offset;
        }

        public void Dispose()
        {
            fs.Close();
        }
        public void Close()
        {
            fs.Close();
        }
    }
}