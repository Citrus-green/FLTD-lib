using System;
using System.IO;

namespace FLTD_lib
{
	class StNIFL
    {
		public NIFL mNIFL;
		public REL0 mREL0;
		public NOF0 mNOF0;

		public StNIFL(BinaryIOHelper fp)
		{
			fp.Seek(0, 0);
			mNIFL = new NIFL(fp);
			fp.Seek((int)mNIFL.rel0_offset, 0);
			mREL0 = new REL0(fp);
			fp.Seek((int)mNIFL.nof0_offset, 0);
			mNOF0 = new NOF0(fp,mNIFL.rel0_offset);
		}
    }
	class NIFL
	{
		public uint tag;
		public uint reserve0;
		public uint reserve1;
		public uint rel0_offset;
		public uint rel0_size;
		public uint nof0_offset;
		public uint nof0_size;
		public uint reserve2;


		public NIFL(BinaryIOHelper fp)
		{
			tag = fp.ReadUInt32();
			reserve0 = fp.ReadUInt32();
			reserve1 = fp.ReadUInt32();
			rel0_offset = fp.ReadUInt32();
			rel0_size = fp.ReadUInt32();
			nof0_offset = fp.ReadUInt32();
			nof0_size = fp.ReadUInt32();
			reserve2 = fp.ReadUInt32();
		}
	};
	class REL0
	{
		public uint tag;
		public uint size;
		public uint entry;
		public uint reserve;

		public REL0(BinaryIOHelper fp) {

			tag = fp.ReadUInt32();
			size = fp.ReadUInt32();
			entry = fp.ReadUInt32();
			reserve = fp.ReadUInt32();
		}
	};
	class NOF0
	{
		public uint tag;
		public uint size;
		public uint count;
		public uint[] addresses;
		public uint[] orgAddresses;

		public NOF0(BinaryIOHelper fp,uint offset) {
			tag = fp.ReadUInt32();
			size = fp.ReadUInt32();
			count = fp.ReadUInt32();

			addresses = new uint[count];
			for(int i=0;i<count;i++)
				addresses[i]= fp.ReadUInt32();
			orgAddresses = this.GetOriginalAddresses(offset);
			for (int i = 0; i < count; i++)
			{
				fp.Seek((int)orgAddresses[i],SeekOrigin.Begin);
				orgAddresses[i] = fp.ReadUInt32();
			}


		}
		public uint[] GetOriginalAddresses(uint offset)
        {
			uint[] buf = new uint[addresses.Length];
			for (int i = 0; i < count; i++)
				buf[i]= addresses[i] + offset;
			return buf;
		}
	};
}
