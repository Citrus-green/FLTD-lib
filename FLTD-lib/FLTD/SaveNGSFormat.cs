using System.Collections.Generic;
using System.IO;

namespace FLTD_lib
{
	public partial class FLTD
	{
		private void SaveNGSFormat(BinaryIOHelper fp)
		{
			// NIFL
			fp.WriteAscii("NIFL");
			fp.WriteUInt32(0X18);
			fp.WriteUInt32(0x1);
			long REL0_OFFSET = fp.Tell();
			fp.WriteUInt32(0);
			long REL0_SIZE = fp.Tell();
			fp.WriteUInt32(0);
			long NOF0_OFFSET = fp.Tell();
			fp.WriteUInt32(0);
			long NOF0_SIZE = fp.Tell();
			fp.WriteUInt32(0);
			fp.WriteUInt32(0X0);

			// REL0
			long temp = fp.Tell();
			fp.Seek((int)REL0_OFFSET, SeekOrigin.Begin);
			fp.WriteUInt32((uint)temp);
			fp.Seek((int)temp, SeekOrigin.Begin);
			REL0_OFFSET = temp;
			fp.WriteAscii("REL0");
			long REL0_CONTENTS_SIZE = fp.Tell();
			fp.WriteUInt32(0);
			long FLTD_OFFSET = fp.Tell();
			fp.WriteUInt32(0);
			fp.WriteUInt32(0x0);

			List<long> NOF0Data = new List<long>();
			List<long> PaddingOffset = new List<long>();
			List<long> EsPaddingOffset = new List<long>();
			List<string> NameFullList = new List<string>();
			List<long> NameOffsetFullList = new List<long>();

			// FLTD
			NOF0Data.Add(fp.Tell() - 0x20);
			fp.WriteUInt32(0xffffffff);

			foreach (NGS.fltd_data0 data0 in data_array.data0)
			{
				data0.name_address_address = (uint)fp.Tell() - 0x20;
				for (int i = 0; i < data0.count_addr0; i++)
				{
					NameFullList.Add(data0.nameList[i]);
					NameOffsetFullList.Add(fp.Tell() - 0x20);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(0);
				}
				if (data0.data3.Length == 0)
					PaddingOffset.Add((uint)fp.Tell() - 0x20);
				foreach (NGS.fltd_data3 data3 in data0.data3)
				{
					if (data3.count_addr1 > 0)
					{
						data3.address1 = (uint)fp.Tell() - 0x20;
						for (int i = 0; i < data3.count_addr1; i++)
						{
							fp.WriteUInt8(data3.unknown[i]);
						}
					}

					while (fp.Tell() % 4 != 0)
						fp.WriteUInt8(0x0);
					if (data3.hasSecondName == 1)
					{
						data3.name_address1 = (uint)fp.Tell() - 0x20;
						NameFullList.Add(data3.name1);
						NameOffsetFullList.Add(fp.Tell() - 0x20);
						NOF0Data.Add(fp.Tell() - 0x20);
						fp.WriteUInt32(0);
					}
				}
				data0.fltd_data3_addr = (uint)fp.Tell() - 0x20;
				for (int i = 0; i < data0.count_addr1; i++)
				{
					NGS.fltd_data3 data3 = data0.data3[i];

					fp.WriteUInt8(data3.format);
					fp.WriteUInt8(data3.count_addr1);
					fp.WriteUInt8(data3.idk2);
					fp.WriteUInt8(data3.idk3);
					foreach (float value in data3.value0)
						fp.WriteFloat(value);

					fp.WriteUInt32(data3.reserve0);
					fp.WriteUInt32(data3.reserve1);
					NameFullList.Add(data3.name0);
					NameOffsetFullList.Add(fp.Tell() - 0x20);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(0);
					if (data3.count_addr1 == 0)
					{
						PaddingOffset.Add(fp.Tell() - 0x20);
					}
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data3.address1);
					PaddingOffset.Add(fp.Tell() - 0x20);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(0);
					if (data3.hasSecondName == 0)
					{
						PaddingOffset.Add(fp.Tell());
					}
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data3.name_address1);

					fp.WriteUInt8(data3.hasSecondName);
					fp.WriteUInt8(data3.value5);
					fp.WriteUInt16(data3.reserve2);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(0x10);
				}
			}
			data_array.fltd_data0_addr = (uint)fp.Tell() - 0x20;
			for (int i = 0; i < data_array.count_addr0; i++)
			{
				NGS.fltd_data0 data0 = (NGS.fltd_data0)data_array.data0[i];
				fp.WriteUInt8((byte)i);
				fp.WriteUInt8(data0.count_addr0);
				fp.WriteUInt8(data0.count_addr1);
				fp.WriteUInt8(0x0);
				NOF0Data.Add(fp.Tell() - 0x20);
				fp.WriteUInt32(data0.name_address_address);
				NOF0Data.Add(fp.Tell() - 0x20);
				fp.WriteUInt32(data0.fltd_data3_addr);
				fp.WriteUInt32(0xFF);
				NOF0Data.Add(fp.Tell() - 0x20);
				fp.WriteUInt32(0x10);
			}
			foreach (NGS.fltd_data1 data1 in data_array.data1)
			{
				foreach (NGS.fltd_data5 data5 in data1.data5)
				{
					data5.fltd_data8_addr = (uint)fp.Tell() - 0x20;
					for (int i = 0; i < data5.data8.Length; i++)
					{
						NGS.fltd_data8 data8 = data5.data8[i];
						fp.WriteUInt8(data8.idk0);
						fp.WriteUInt8(data8.idk1);
						fp.WriteUInt8(data8.idk2);
						fp.WriteUInt8(data8.idk3);

						fp.WriteUInt32(0x0);
						fp.WriteUInt32(0x0);
						fp.WriteUInt32(data8.value2);
						fp.WriteUInt32(data8.value0);
						fp.WriteUInt32(data8.value1);
					}
					data5.fltd_data9_addr = (uint)fp.Tell() - 0x20;
					for (int i = 0; i < data5.data9.Length; i++)
					{
						NGS.fltd_data9 data9 = data5.data9[i];
						fp.WriteUInt8(data9.idk0);
						fp.WriteUInt8(data9.idk1);
						fp.WriteUInt8(data9.idk2);
						fp.WriteUInt8(data9.idk3);

						fp.WriteUInt32(0x0);
						fp.WriteUInt32(0x0);
						fp.WriteUInt32(data9.value0);
						fp.WriteUInt32(data9.value1);
						fp.WriteUInt32(data9.value2);
					}
					data5.fltd_data10_addr = (uint)fp.Tell() - 0x20;
					for (int i = 0; i < data5.count_addr3; i++)
					{
						NGS.fltd_data10 data10 = data5.data10[i];
						fp.WriteUInt8(data10.idk0);
						fp.WriteUInt8(data10.idk1);
						fp.WriteUInt8(data10.idk2);
						fp.WriteUInt8(data10.idk3);

						fp.WriteUInt32(0x0);
						fp.WriteUInt32(0x0);
						fp.WriteUInt32(0x0);
						fp.WriteUInt32(data10.value0);
					}
					data5.fltd_data11_addr = (uint)fp.Tell() - 0x20;
					for(int i=0;i< data5.data11.Length;i++)
						fp.WriteFloat(((float)data5.data11.Length-i)/ data5.data11.Length);

					data5.fltd_data7_addr = (uint)fp.Tell() - 0x20;
					for (int i = 0; i < data5.data7.Length; i++)
					{
						fp.WriteUInt8(0x3);
					}
					while (fp.Tell() % 4 != 0)
						fp.WriteUInt8(0x0);

					data5.data12.fltd_data13_addr = (uint)fp.Tell() - 0x20;
					foreach (NGS.fltd_data13 data13 in data5.data12.data13)
					{
						foreach (float f in data13.value0)
						{
							fp.WriteFloat(f);
						}
						fp.WriteUInt32(0x1);
						fp.WriteUInt32(0x0);
						NOF0Data.Add(fp.Tell() - 0x20);
						fp.WriteUInt32(0x10);
					}
					data5.data12.fltd_data14_addr = (uint)fp.Tell() - 0x20;
					foreach (NGS.fltd_data14 data14 in data5.data12.data14)
					{
						fp.WriteUInt32(data14.value0);
						fp.WriteUInt32(data14.value1);
						fp.WriteUInt32(data14.value2);
						fp.WriteUInt32(0x0);
						NOF0Data.Add(fp.Tell() - 0x20);
						fp.WriteUInt32(0x10);
					}
					data5.fltd_data12_addr = (uint)fp.Tell() - 0x20;

					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data5.data12.fltd_data13_addr);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data5.data12.fltd_data14_addr);
					fp.WriteUInt32(0x0);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(0x10);
				}
				data1.fltd_data5_addr = (uint)fp.Tell() - 0x20;
				for (int i = 0; i < data1.data5.Length; i++)
				{
					NGS.fltd_data5 data5 = data1.data5[i];
					fp.WriteUInt32(0x3DCCCCCD);
					fp.WriteUInt32(0x0);
					fp.WriteUInt32(0x3F666666);
					fp.WriteUInt8(0x20);
					fp.WriteUInt8(0x1);
					fp.WriteUInt8(data5.idk2);
					fp.WriteUInt8(data5.idk3);
					fp.WriteUInt8(data5.idk4);
					fp.WriteUInt8((byte)data5.data8.Length);
					fp.WriteUInt8((byte)data5.data9.Length);
					fp.WriteUInt8((byte)data5.data10.Length);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data5.fltd_data7_addr);
					fp.WriteUInt32(0x0);
					fp.WriteUInt32(0x0);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data5.fltd_data8_addr);
					if(data5.data9.Length==0)
						PaddingOffset.Add(fp.Tell() - 0x20);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data5.fltd_data9_addr);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data5.fltd_data10_addr);

					for (int j = 0; j < data5.parent.count_fltd_data13; j++)
					{
						PaddingOffset.Add(fp.Tell() - 0x20);
					}
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(0);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data5.fltd_data11_addr);
					fp.WriteUInt32(0x0);
					NOF0Data.Add(fp.Tell() - 0x20);
					fp.WriteUInt32(data5.fltd_data12_addr);
				}

				data1.fltd_data6_addr = (uint)fp.Tell() - 0x20;
				NGS.fltd_data6 data6 = data1.data6;
				fp.WriteUInt32(0x0);
				fp.WriteFloat(1.0f);
				for (int j = 0; j < data6.parent.count_fltd_data13*4; j++)
				{
					PaddingOffset.Add(fp.Tell() - 0x20);
				}
				NOF0Data.Add(fp.Tell() - 0x20);
				fp.WriteUInt32(0);

				data1.fltd_data4_addr = (uint)fp.Tell() - 0x20;
				if (data1.data4.Length > 1)
				{
					for (int i = 0; i < data1.data4.Length; i++)
					{
						fp.WriteUInt8((byte)(i * 4));
					}
                }
                else
                {
					PaddingOffset.Add(-1);
				}
				while (fp.Tell() % 4 != 0)
					fp.WriteUInt8(0x0);


			}
			data_array.fltd_data1_addr = (uint)fp.Tell() - 0x20;
			foreach (NGS.fltd_data1 data1 in data_array.data1)
			{
				fp.WriteUInt8(0x1f);
				fp.WriteUInt8(data1.count_fltd_data13); // TODO
				fp.WriteUInt8(data1.idk2);
				fp.WriteUInt8(data1.idk3);
				fp.WriteUInt8((byte)data1.data4.Length);
				fp.WriteUInt8((byte)data1.data5.Length);
				fp.WriteUInt8(data1.idk6);
				fp.WriteUInt8(data1.idk7);

				if (data1.data4.Length == 1)
					EsPaddingOffset.Add(fp.Tell() - 0x20);
				NOF0Data.Add(fp.Tell() - 0x20);
				fp.WriteUInt32(data1.fltd_data4_addr);
				NOF0Data.Add(fp.Tell() - 0x20);
				fp.WriteUInt32(data1.fltd_data5_addr);
				NOF0Data.Add(fp.Tell() - 0x20);
				fp.WriteUInt32(data1.fltd_data6_addr);

				fp.WriteUInt32(data1.value0);
				NOF0Data.Add(fp.Tell() - 0x20);
				fp.WriteUInt32(data1.address3);
			}

			data_array.fltd_data2_addr = (uint)fp.Tell() - 0x20;
			fp.WriteUInt32(0x0);
			PaddingOffset.Add((uint)fp.Tell() - 0x20);
			NOF0Data.Add(fp.Tell() - 0x20);
			fp.WriteUInt32(0);
			fp.WriteUInt32(0x0);
			NOF0Data.Add(fp.Tell() - 0x20);
			fp.WriteUInt32(0x10);
			
			temp = fp.Tell();
			fp.Seek((int)FLTD_OFFSET,SeekOrigin.Begin);
			fp.WriteUInt32((uint)temp - 0x20);
			fp.Seek((int)temp, SeekOrigin.Begin);
			fp.WriteUInt8(0xA);
			fp.WriteUInt8((byte)data_array.data0.Length);
			fp.WriteUInt8((byte)data_array.data1.Length);
			fp.WriteUInt8(0x7);
			NOF0Data.Add(fp.Tell() - 0x20);
			fp.WriteUInt32(data_array.fltd_data0_addr);
			NOF0Data.Add(fp.Tell() - 0x20);
			fp.WriteUInt32(data_array.fltd_data1_addr);
			fp.WriteUInt32(0x0);
			fp.WriteUInt32(data_array.fltd_data2_addr);
			
			for (int i = 0; i < NameFullList.Count; i++)
			{
				bool flag = false;
				for (int j = 0; j < i; j++)
				{
					if (NameFullList[i] == NameFullList[j])
					{
						temp = fp.Tell() - 0x20;
						fp.SkipSeek((int)NameOffsetFullList[i]);
						fp.WriteUInt32((uint)NameOffsetFullList[j]);
						fp.SkipSeek((int)temp);
						flag = !flag;
						break;
					}
				}
                if (!flag) { 
				temp = fp.Tell() - 0x20;
				fp.SkipSeek((int)NameOffsetFullList[i]);
					fp.WriteUInt32((uint)temp);
				fp.SkipSeek((int)temp);
				NameOffsetFullList[i] = temp;
				fp.WriteAscii(NameFullList[i]);
				fp.WriteUInt8(0x0);
				while (fp.Tell() % 0x4 != 0)
					fp.WriteUInt8(0x0);
				}
			}

			// unknown data
			fp.WriteUInt32(0x0);
			fp.WriteUInt32(0x1);
			fp.WriteUInt8(0x1);
			for (int i=1;i<=0x80;i*=2)
				fp.WriteUInt8((byte)i);
			while (fp.Tell() % 0x4 != 0)
				fp.WriteUInt8(0x0);
			for (int i = 1; i <= 0x40; i*=2)
				fp.WriteUInt32((byte)i);
			fp.WriteUInt32(0x01100801);

			temp = fp.Tell();
			fp.Seek((int)REL0_CONTENTS_SIZE,SeekOrigin.Begin);
			fp.WriteUInt32((uint)temp - 0x20);
			fp.Seek((int)temp, SeekOrigin.Begin);

			temp = fp.Tell();
			fp.Seek((int)REL0_SIZE,SeekOrigin.Begin);
			fp.WriteUInt32((uint)(temp - REL0_OFFSET));
			fp.Seek((int)temp, SeekOrigin.Begin);

			int EsCounter = 0;

			for (int i = 0; i < PaddingOffset.Count; i++)
			{
				temp = fp.Tell() - 0x20;
				if (PaddingOffset[i] < 0)
				{
					fp.SkipSeek((int)EsPaddingOffset[EsCounter++]);
					fp.WriteUInt32((uint)temp);
					fp.SkipSeek((int)temp);
					fp.WriteUInt32(0);
				}
				else
				{
					fp.SkipSeek((int)PaddingOffset[i]);
					fp.WriteUInt32((uint)temp);
					fp.SkipSeek((int)temp);
					int index = i;
					do
					{
						fp.WriteUInt32(0);
					} while (++index < PaddingOffset.Count && PaddingOffset[i] == PaddingOffset[index]);
					i = index - 1;
				}
			}

			temp = fp.Tell();
			fp.Seek((int)NOF0_OFFSET,SeekOrigin.Begin);
			fp.WriteUInt32((uint)temp);
			fp.Seek((int)temp, SeekOrigin.Begin);
			NOF0_OFFSET = temp;
			fp.WriteAscii("NOF0");
			fp.WriteUInt32((uint)(0x4 + NOF0Data.Count*4)) ;//count*4+0x4
			fp.WriteUInt32((uint)NOF0Data.Count);//count
			NOF0Data.Sort();
			foreach (uint address in NOF0Data)
				fp.WriteUInt32(address);
			while (fp.Tell() % 0x10 != 0)
				fp.WriteUInt32(0x0);
			temp = fp.Tell();
			fp.Seek((int)NOF0_SIZE,SeekOrigin.Begin);
			fp.WriteUInt32((uint)(temp - NOF0_OFFSET));
			fp.Seek((int)temp, SeekOrigin.Begin);

			fp.WriteAscii("NEND");
			fp.WriteUInt32(0x8);
			fp.WriteUInt32(0x0);
			fp.WriteUInt32(0x0);
		}
	}
}
