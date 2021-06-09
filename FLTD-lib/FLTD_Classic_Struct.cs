namespace FLTD_lib
{
	namespace Classic {
		internal class fltd_data0
		{
			public StFltd parent;

			public byte index;
			public byte count_addr0;
			public byte count_addr1;
			public byte reserve; // 0x00

			public uint name_address_address; // size 4
			public uint fltd_data3_addr; // size 5c
			public uint value0;
			public uint address; //0x10

			public uint[] nameAddressList;
			public string[] nameList;
			public fltd_data3[] data3;

			public fltd_data0(BinaryIOHelper fp, StFltd parent)
			{
				this.parent = parent;
				index = fp.ReadUInt8();
				count_addr0 = fp.ReadUInt8();
				count_addr1 = fp.ReadUInt8();
				reserve = fp.ReadUInt8();
				name_address_address = fp.ReadUInt32();
				fltd_data3_addr = fp.ReadUInt32();
				value0 = fp.ReadUInt32();
				address = fp.ReadUInt32();

				nameAddressList = new uint[count_addr0];
				nameList = new string[count_addr0];
				for (int i = 0; i < count_addr0; i++)
				{
					fp.SkipSeek((int)name_address_address + i * 4);
					nameAddressList[i] = fp.ReadUInt32();
					fp.SkipSeek((int)nameAddressList[i]);
					nameList[i] = fp.ReadAscii();
				}

				data3 = new fltd_data3[this.count_addr1];
				for (int i = 0; i < count_addr1; i++)
				{
					fp.SkipSeek((int)fltd_data3_addr + fltd_data3.GetMyDataSize() * i);
					data3[i] = new fltd_data3(fp, this);
				}
			}
			static public int GetMyDataSize()
			{
				return 0x14;
			}
		};

		internal class fltd_data1
		{
			public StFltd parent;

			public byte idk0; // 0x1f
			public byte count_fltd_data13; // in fltd_data12
			public byte idk2;
			public byte idk3;
			public byte count_addr0;
			public byte count_addr1;
			public byte count_addr2;
			public byte idk7;

			public uint fltd_data4_addr; // addr0
			public uint fltd_data5_addr; // addr1
			public uint fltd_data6_addr; // addr2

			public uint value0; // 0x1
			public uint address; // 0x10

			public fltd_data4[] data4;

			public fltd_data1(BinaryIOHelper fp, StFltd parent)
			{
				this.parent = parent;
				idk0 = fp.ReadUInt8();
				count_fltd_data13 = fp.ReadUInt8();
				idk2 = fp.ReadUInt8();
				idk3 = fp.ReadUInt8();
				count_addr0 = fp.ReadUInt8();
				count_addr1 = fp.ReadUInt8();
				count_addr2 = fp.ReadUInt8();
				idk7 = fp.ReadUInt8();
				fltd_data4_addr = fp.ReadUInt32();
				fltd_data5_addr = fp.ReadUInt32();
				fltd_data6_addr = fp.ReadUInt32();
				value0 = fp.ReadUInt32();
				address = fp.ReadUInt32();

				data4 = new fltd_data4[this.count_addr1];
				for (int i = 0; i < count_addr1; i++)
				{
					fp.SkipSeek((int)fltd_data4_addr + fltd_data3.GetMyDataSize() * i);
					data4[i] = new fltd_data4(fp, this);
				}
			}

			static public int GetMyDataSize()
			{
				return 0x1c;
			}
		};

		internal class fltd_data2
		{
			public StFltd parent;
			public uint value0; // 0xffffffff

			public fltd_data2(BinaryIOHelper fp, StFltd parent)
			{
				this.parent = parent;
				value0 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return 4;
			}
		};

		internal class fltd_data3
		{
			public fltd_data0 parent;

			public byte idk0;
			public byte idk1;
			public byte idk2; // 0x0
			public byte idk3;
			public float[] value0;

			public uint name_address0;
			public uint address1; // go to reserve: size 4
			public uint address2; // go to reserve:size 4


			public string name;

			public fltd_data3(BinaryIOHelper fp, fltd_data0 parent)
			{
				this.parent = parent;
				value0 = new float[0xa];

				idk0 = fp.ReadUInt8();
				idk1 = fp.ReadUInt8();
				idk2 = fp.ReadUInt8();
				idk3 = fp.ReadUInt8();

				for (int i = 0; i < 0xa; i++)
					value0[i] = fp.ReadFloat();

				name_address0 = fp.ReadUInt32();
				address1 = fp.ReadUInt32();
				address2 = fp.ReadUInt32();

				fp.SkipSeek((int)name_address0);
				name = fp.ReadAscii();
			}

			static public int GetMyDataSize()
			{
				return 0x38;
			}
		};

		internal class fltd_data4
		{
			public fltd_data1 parent;

			public float value0;
			public float value1;
			public float value2;
			public uint idk0;
			public byte count0;
			public byte count1;
			public byte count2;
			public byte count3;
			public uint address0;
			public uint reserve0;
			public uint reserve1;
			public uint address1;
			public uint address2;
			public uint address3;
			public uint address4;
			public uint address5;
			public uint reserve2;
			public uint address6;

			public fltd_data4(BinaryIOHelper fp, fltd_data1 parent)
			{
				this.parent = parent;
				value0 = fp.ReadFloat();
				value1 = fp.ReadFloat();
				value2 = fp.ReadFloat();
				idk0 = fp.ReadUInt32();
				count0 = fp.ReadUInt8();
				count1 = fp.ReadUInt8();
				count2 = fp.ReadUInt8();
				count3 = fp.ReadUInt8();
				address0 = fp.ReadUInt32();
				reserve0 = fp.ReadUInt32();
				reserve1 = fp.ReadUInt32();
				address1 = fp.ReadUInt32();
				address2 = fp.ReadUInt32();
				address3 = fp.ReadUInt32();
				address4 = fp.ReadUInt32();
				address5 = fp.ReadUInt32();
				reserve2 = fp.ReadUInt32();
				address6 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return 0x3c;
			}
		};
	};
}
