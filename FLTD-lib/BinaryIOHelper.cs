using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLTD_lib
{
    internal class BinaryIOHelper:IDisposable
    {
        private FileStream fs;
        private long skip;
        private bool littleEdian;
        public BinaryIOHelper(FileStream fs,bool b)
        {
            this.fs = fs;
            littleEdian = b;
            skip = 0;
        }
        public long Seek(int offset,SeekOrigin origin) {
            return fs.Seek(offset, origin);
        }

        public long SkipSeek(int offset)
        {
            return fs.Seek(offset+skip, 0);
        }
        public uint ReadUInt32()
        {
            byte[] buf = new byte[sizeof(uint)];
            fs.Read(buf,0,sizeof(uint));
            if (littleEdian)
                Array.Reverse(buf);
            return BitConverter.ToUInt32(buf, 0);
        }
        public byte ReadUInt8()
        {
            byte[] buf = new byte[sizeof(byte)];
            fs.Read(buf,0, sizeof(byte));
            return buf[0];
        }
        unsafe public float ReadFloat()
        {
            byte[] buf = new byte[sizeof(float)];
            fs.Read(buf,0, sizeof(float));
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
        public void SetSkip(long offset)
        {
            skip = offset;
        }

        public void Dispose()
        {
            fs.Close();
        }
        public void Close() {
            fs.Close();
        }
    }
}
