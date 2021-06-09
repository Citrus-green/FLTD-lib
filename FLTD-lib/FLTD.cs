using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FLTD_lib
{
	internal class StFltd
	{
		public byte format;
		public byte count_addr0;
		public byte count_addr1;
		public byte idk3; // count_addr2 ??
		public uint fltd_data0_addr;
		public uint fltd_data1_addr;
		public uint reserve;
		public uint fltd_data2_addr;

		public object[] data0;
		public object[] data1;
		public object data2;

		public StFltd(BinaryIOHelper fp)
		{
			format = fp.ReadUInt8();
			count_addr0 = fp.ReadUInt8();
			count_addr1 = fp.ReadUInt8();
			idk3 = fp.ReadUInt8();
			fltd_data0_addr = fp.ReadUInt32();
			fltd_data1_addr = fp.ReadUInt32();
			reserve = fp.ReadUInt32();
			fltd_data2_addr = fp.ReadUInt32();

			if (IsNGS() == true)
			{
				data0 = new NGS.fltd_data0[count_addr0];
				data1 = new NGS.fltd_data1[count_addr1];
				for (int i = 0; i < count_addr0; i++)
				{
					fp.SkipSeek((int)fltd_data0_addr + NGS.fltd_data0.GetMyDataSize() * i);
					data0[i] = new NGS.fltd_data0(fp, this);
				}
				for (int i = 0; i < count_addr1; i++)
				{
					fp.SkipSeek((int)fltd_data1_addr + NGS.fltd_data1.GetMyDataSize() * i);
					data1[i] = new NGS.fltd_data1(fp, this);
				}
				fp.SkipSeek((int)fltd_data2_addr);
				data2 = new NGS.fltd_data2(fp, this);
			}
			else {
				data0 = new Classic.fltd_data0[count_addr0];
				data1 = new Classic.fltd_data1[count_addr1];
				for (int i = 0; i < count_addr0; i++)
				{
					fp.SkipSeek((int)fltd_data0_addr + Classic.fltd_data0.GetMyDataSize() * i);
					data0[i] = new Classic.fltd_data0(fp, this);
				}
				for (int i = 0; i < count_addr1; i++)
				{
					fp.SkipSeek((int)fltd_data1_addr + Classic.fltd_data1.GetMyDataSize() * i);
					data1[i] = new Classic.fltd_data1(fp, this);
				}
				fp.SkipSeek((int)fltd_data2_addr);
				data2 = new Classic.fltd_data2(fp, this);
			}
		}
		static public int GetMyDataSize()
		{
			return sizeof(byte) * 4 + sizeof(uint) * 4;
		}
		public bool IsNGS()
		{
			return (format == 0xA);
		}
	};

	public class FLTD
	{
		
		private StFltd data_array;
		private StNIFL mStNIFL;

		public FLTD()
		{

		}
		public void loadFile(String path)
		{
			using (var fp = new BinaryIOHelper(new FileStream(path, FileMode.Open, FileAccess.Read), false))
			{
				mStNIFL = new StNIFL(fp);
				fp.SetSkip(mStNIFL.mNIFL.rel0_offset);
				fp.SkipSeek((int)mStNIFL.mREL0.entry);
				data_array = new StFltd(fp);
			}
		}
		private void readStFltd(BinaryReader fp)
		{

		}
		public string[] GetAssignList()
		{
			string[] str = new string[data_array.count_addr0];

			if (data_array.IsNGS() == true)
			{
				for (int i = 0; i < data_array.count_addr0; i++)
					str[i] = ((NGS.fltd_data0)data_array.data0[i]).nameList[0];
				return str;
			}

			for (int i = 0; i < data_array.count_addr0; i++)
				str[i] = ((Classic.fltd_data0)data_array.data0[i]).nameList[0];
			return str;
		}
		public string[] GetRootMode(int index)
		{
			if (data_array.IsNGS() == true)
				return ((NGS.fltd_data0)data_array.data0[index]).nameList;

			return ((Classic.fltd_data0)data_array.data0[index]).nameList;
		}
		public string[] GetConstraintList(int index)
		{
			if (data_array.IsNGS() == true)
			{
				NGS.fltd_data0 data0 = (NGS.fltd_data0)data_array.data0[index];
				string[] nameList = new string[data0.count_addr1];
				for (int i = 0; i < data0.count_addr1; i++)
				{
					nameList[i] = data0.data3[i].name;
				}
				return nameList;
			}
			else { 
				Classic.fltd_data0 data0 = (Classic.fltd_data0)data_array.data0[index];
				string[] nameList = new string[data0.count_addr1];
				for (int i = 0; i < data0.count_addr1; i++)
				{
					nameList[i] = data0.data3[i].name;
				}
				return nameList;
			}
		}

	}
}
