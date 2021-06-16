namespace FLTD_lib
{
	namespace NGS
	{

		internal class fltd_data0
		{
			public StFltd parent;

			public byte index;
			public byte count_addr0;
			public byte count_addr1;
			public byte reserve; // 0x00

			public uint name_address_address; // size 4
			public uint fltd_data3_addr; // size 5c
			public uint value0; // 0xff
			public uint value1; // 0x10

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
				value1 = fp.ReadUInt32();

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
				return sizeof(byte) * 4 + sizeof(uint) * 4;
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
			public byte idk6;
			public byte idk7;

			public uint fltd_data4_addr; // addr0
			public uint fltd_data5_addr; // addr1
			public uint fltd_data6_addr; // addr2

			public uint value0; // 0x1
			public uint value1; // 0x10

			public fltd_data4[] data4;
			public fltd_data5[] data5;
			public fltd_data6 data6;

			public fltd_data1(BinaryIOHelper fp, StFltd parent)
			{
				this.parent = parent;
				idk0 = fp.ReadUInt8();
				count_fltd_data13 = fp.ReadUInt8();
				idk2 = fp.ReadUInt8();
				idk3 = fp.ReadUInt8();
				count_addr0 = fp.ReadUInt8();
				count_addr1 = fp.ReadUInt8();
				idk6 = fp.ReadUInt8();
				idk7 = fp.ReadUInt8();
				fltd_data4_addr = fp.ReadUInt32();
				fltd_data5_addr = fp.ReadUInt32();
				fltd_data6_addr = fp.ReadUInt32();
				value0 = fp.ReadUInt32();
				value1 = fp.ReadUInt32();

				data4 = new fltd_data4[this.count_addr0];
				for (int i = 0; i < this.count_addr0; i++)
				{
					fp.SkipSeek((int)fltd_data4_addr + fltd_data4.GetMyDataSize() * i);
					data4[i] = new fltd_data4(fp, this);
				}
				data5 = new fltd_data5[this.count_addr1];
				for (int i = 0; i < this.count_addr1; i++)
				{
					fp.SkipSeek((int)fltd_data5_addr + fltd_data5.GetMyDataSize() * i);
					data5[i] = new fltd_data5(fp, this);
				}
				fp.SkipSeek((int)fltd_data6_addr);
				data6 = new fltd_data6(fp, this);
			}

			static public int GetMyDataSize()
			{
				return sizeof(byte) * 8 + sizeof(uint) * 5;
			}
		};

		internal class fltd_data2
		{
			public StFltd parent;
			public uint value0; // 0x0

			public uint address0; // fin addr??

			public uint value1; // 0x0
			public uint value2; // 0x10

			public fltd_data2(BinaryIOHelper fp, StFltd parent)
			{
				this.parent = parent;
				value0 = fp.ReadUInt32();
				address0 = fp.ReadUInt32();
				value1 = fp.ReadUInt32();
				value2 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return sizeof(uint) * 4;
			}
		};

		internal class fltd_data3
		{
			public fltd_data0 parent;

			public byte idk0;
			public byte count_addr1;
			public byte idk2; // 0x0
			public byte idk3;
			public float[] value0;
			public uint reserve0;
			public uint reserve1;

			public uint name_address0; // relation??
			public uint address1; // go to reserve
			public uint address2; // go to reserve
			public uint name_address1; // go name address address (relation??)

			public byte hasSecondName;
			public byte value5;
			public ushort reserve2;
			public uint address4; // 0x10

			public string name0;
			public string name1;
			public byte[] unknown;

			public fltd_data3(BinaryIOHelper fp, fltd_data0 parent)
			{
				this.parent = parent;
				value0 = new float[0xe];

				idk0 = fp.ReadUInt8();
				count_addr1 = fp.ReadUInt8();
				idk2 = fp.ReadUInt8();
				idk3 = fp.ReadUInt8();

				for (int i = 0; i < 0xe; i++)
					value0[i] = fp.ReadFloat();

				reserve0 = fp.ReadUInt32();
				reserve1 = fp.ReadUInt32();
				name_address0 = fp.ReadUInt32();
				address1 = fp.ReadUInt32();
				address2 = fp.ReadUInt32();
				name_address1 = fp.ReadUInt32();
				hasSecondName = fp.ReadUInt8();
				value5 = fp.ReadUInt8();
				reserve2 = fp.ReadUInt16();
				address4 = fp.ReadUInt32();

				fp.SkipSeek((int)name_address0);
				name0 = fp.ReadAscii();
				if (hasSecondName == 1)
				{
					fp.SkipSeek((int)name_address1);
					fp.SkipSeek((int)fp.ReadUInt32());
					name1 = fp.ReadAscii();
				}
				else
					name1 = "";

				unknown = new byte[count_addr1];
				fp.SkipSeek((int)address1);
				for (int i = 0; i < count_addr1; i++)
                {
					unknown[i] = fp.ReadUInt8();
                }
			}

			static public int GetMyDataSize()
			{
				return sizeof(byte) * 4 + sizeof(uint) * 8 + sizeof(float) * 0xe;
			}
		};

		internal class fltd_data4
		{
			public fltd_data1 parent;

			public byte value;

			public fltd_data4(BinaryIOHelper fp, fltd_data1 parent)
			{
				this.parent = parent;
				value = fp.ReadUInt8();
			}
			static public int GetMyDataSize()
			{
				return sizeof(byte);
			}
		};

		internal class fltd_data5
		{
			public fltd_data1 parent;

			public uint value0; // 0x3DCCCCCD
			public uint reserve0;
			public uint value1; //0x3f666666
			public byte idk0;
			public byte idk1;
			public byte idk2;
			public byte idk3;
			public byte count_addr0; // data type??
			public byte count_addr1; // and addr0??
			public byte count_addr2; // and addr5??
			public byte count_addr3;
			public uint fltd_data7_addr; // addr0
			public uint reserve1;
			public uint reserve2;
			public uint fltd_data8_addr; // addr1
			public uint fltd_data9_addr; // addr2
			public uint fltd_data10_addr; // addr3
			public uint address4; // goto reserve
			public uint fltd_data11_addr; // addr5
			public uint reserve3;
			public uint fltd_data12_addr; // addr6

			public fltd_data7[] data7;
			public fltd_data8[] data8;
			public fltd_data9[] data9;
			public fltd_data10[] data10;

			public fltd_data11 data11;
			public fltd_data12 data12;

			public fltd_data5(BinaryIOHelper fp, fltd_data1 parent)
			{
				this.parent = parent;
				value0 = fp.ReadUInt32();
				reserve0 = fp.ReadUInt32();
				value1 = fp.ReadUInt32();
				idk0 = fp.ReadUInt8();
				idk1 = fp.ReadUInt8();
				idk2 = fp.ReadUInt8();
				idk3 = fp.ReadUInt8();
				count_addr0 = fp.ReadUInt8();
				count_addr1 = fp.ReadUInt8();
				count_addr2 = fp.ReadUInt8();
				count_addr3 = fp.ReadUInt8();
				fltd_data7_addr = fp.ReadUInt32();
				reserve1 = fp.ReadUInt32();
				reserve2 = fp.ReadUInt32();
				fltd_data8_addr = fp.ReadUInt32();
				fltd_data9_addr = fp.ReadUInt32();
				fltd_data10_addr = fp.ReadUInt32();
				address4 = fp.ReadUInt32();
				fltd_data11_addr = fp.ReadUInt32();
				reserve3 = fp.ReadUInt32();
				fltd_data12_addr = fp.ReadUInt32();

				data7 = new fltd_data7[this.count_addr0];
				for (int i = 0; i < this.count_addr0; i++)
				{
					fp.SkipSeek((int)fltd_data7_addr + fltd_data7.GetMyDataSize() * i);
					data7[i] = new fltd_data7(fp, this);
				}

				data8 = new fltd_data8[this.count_addr1];
				for (int i = 0; i < this.count_addr1; i++)
				{
					fp.SkipSeek((int)fltd_data8_addr + fltd_data8.GetMyDataSize() * i);
					data8[i] = new fltd_data8(fp, this);
				}

				data9 = new fltd_data9[this.count_addr2];
				for (int i = 0; i < this.count_addr2; i++)
				{
					fp.SkipSeek((int)fltd_data9_addr + fltd_data9.GetMyDataSize() * i);
					data9[i] = new fltd_data9(fp, this);
				}

				data10 = new fltd_data10[this.count_addr3];
				for (int i = 0; i < this.count_addr3; i++)
				{
					fp.SkipSeek((int)fltd_data10_addr + fltd_data10.GetMyDataSize() * i);
					data10[i] = new fltd_data10(fp, this);
				}

				fp.SkipSeek((int)fltd_data11_addr);
				data11 = new fltd_data11(fp, this);
				fp.SkipSeek((int)fltd_data12_addr);
				data12 = new fltd_data12(fp, this);
			}

			static public int GetMyDataSize()
			{
				return sizeof(byte) * 8 + sizeof(uint) * 0xd;
			}
		};

		internal class fltd_data6
		{
			public fltd_data1 parent;
			public uint reserve;
			public uint value0; // 0x3f800000
			public uint value1;

			public fltd_data6(BinaryIOHelper fp, fltd_data1 parent)
			{
				this.parent = parent;
				reserve = fp.ReadUInt32();
				value0 = fp.ReadUInt32();
				value1 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return sizeof(uint) * 3;
			}
		};

		internal class fltd_data7
		{
			public fltd_data5 parent;

			public uint value;

			public fltd_data7(BinaryIOHelper fp, fltd_data5 parent)
			{
				this.parent = parent;
				value = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return sizeof(uint);
			}
		};

		internal class fltd_data8
		{
			public fltd_data5 parent;

			public byte idk0;
			public byte idk1;
			public byte idk2;
			public byte idk3;

			public uint reserve0;
			public uint reserve1;
			public uint value2;

			public uint value0;
			public uint value1; // (!value0)?0x28:0x5a

			public fltd_data8(BinaryIOHelper fp, fltd_data5 parent)
			{
				this.parent = parent;
				idk0 = fp.ReadUInt8();
				idk1 = fp.ReadUInt8();
				idk2 = fp.ReadUInt8();
				idk3 = fp.ReadUInt8();
				reserve0 = fp.ReadUInt32();
				reserve1 = fp.ReadUInt32();
				value2 = fp.ReadUInt32();
				value0 = fp.ReadUInt32();
				value1 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return sizeof(byte) * 4 + sizeof(uint) * 5;
			}
		};

		internal class fltd_data9
		{
			public fltd_data5 parent;

			public byte idk0;
			public byte idk1;
			public byte idk2;
			public byte idk3;

			public uint reserve0;
			public uint reserve1;

			public uint value0;
			public uint value1;
			public uint value2; // 0x5a or 23

			public fltd_data9(BinaryIOHelper fp, fltd_data5 parent)
			{
				this.parent = parent;
				idk0 = fp.ReadUInt8();
				idk1 = fp.ReadUInt8();
				idk2 = fp.ReadUInt8();
				idk3 = fp.ReadUInt8();
				reserve0 = fp.ReadUInt32();
				reserve1 = fp.ReadUInt32();
				value0 = fp.ReadUInt32();
				value1 = fp.ReadUInt32();
				value2 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return sizeof(byte) * 4 + sizeof(uint) * 5;
			}
		};

		internal class fltd_data10
		{
			public fltd_data5 parent;

			public byte idk0;
			public byte idk1;
			public byte idk2;
			public byte idk3;

			public uint reserve0;
			public uint reserve1;
			public uint reserve2;

			public uint value0;

			public fltd_data10(BinaryIOHelper fp, fltd_data5 parent)
			{
				this.parent = parent;
				idk0 = fp.ReadUInt8();
				idk1 = fp.ReadUInt8();
				idk2 = fp.ReadUInt8();
				idk3 = fp.ReadUInt8();
				reserve0 = fp.ReadUInt32();
				reserve1 = fp.ReadUInt32();
				reserve2 = fp.ReadUInt32();
				value0 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return sizeof(byte) * 4 + sizeof(uint) * 4;
			}
		};

		internal class fltd_data11
		{
			public fltd_data5 parent;

			public uint value0;
			public uint value1;

			public fltd_data11(BinaryIOHelper fp, fltd_data5 parent)
			{
				this.parent = parent;
				value0 = fp.ReadUInt32();
				value1 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return sizeof(uint) * 2;
			}
		};

		internal class fltd_data12
		{
			public fltd_data5 parent;

			public uint fltd_data13_addr; // fltd_data1->count_fltd_data13
			public uint fltd_data14_addr; // fltd_data5->count_addr3

			public uint reserve;
			public uint value; // 0x10

			public fltd_data13[] data13;
			public fltd_data14[] data14;

			public fltd_data12(BinaryIOHelper fp, fltd_data5 parent)
			{
				this.parent = parent;
				fltd_data13_addr = fp.ReadUInt32();
				fltd_data14_addr = fp.ReadUInt32();
				reserve = fp.ReadUInt32();
				value = fp.ReadUInt32();

				data13 = new fltd_data13[parent.parent.count_fltd_data13];
				for (int i = 0; i < parent.parent.count_fltd_data13; i++)
				{
					fp.SkipSeek((int)fltd_data13_addr + fltd_data13.GetMyDataSize() * i);
					data13[i] = new fltd_data13(fp, this);
				}

				data14 = new fltd_data14[parent.count_addr3];
				for (int i = 0; i < parent.count_addr3; i++)
				{
					fp.SkipSeek((int)fltd_data14_addr + fltd_data14.GetMyDataSize() * i);
					data14[i] = new fltd_data14(fp, this);
				}
			}

			static public int GetMyDataSize()
			{
				return sizeof(uint) * 4;
			}
		};

		internal class fltd_data13
		{
			public fltd_data12 parent;

			public float[] value0;
			public uint value1; //0x1
			public uint value2; //0x0
			public uint value3; //0x10

			public fltd_data13(BinaryIOHelper fp, fltd_data12 parent)
			{
				this.parent = parent;
				value0 = new float[5];
				for (int i = 0; i < 5; i++)
					value0[i] = fp.ReadFloat();
				value1 = fp.ReadUInt32();
				value2 = fp.ReadUInt32();
				value3 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return sizeof(float) * 5 + sizeof(uint) * 3;
			}
		};

		internal class fltd_data14
		{
			public uint value0;
			public uint value1;

			public uint value2;
			public uint reserve2;
			public uint value3; // 0x10

			public fltd_data12 parent;

			public fltd_data14(BinaryIOHelper fp, fltd_data12 parent)
			{
				this.parent = parent;
				value0 = fp.ReadUInt32();
				value1 = fp.ReadUInt32();
				value2 = fp.ReadUInt32();
				reserve2 = fp.ReadUInt32();
				value3 = fp.ReadUInt32();
			}
			static public int GetMyDataSize()
			{
				return sizeof(uint) * 5;
			}
		};
	};
};