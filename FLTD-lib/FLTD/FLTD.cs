using System;
using System.Collections.Generic;
using System.IO;

namespace FLTD_lib
{

	public partial class FLTD
	{

		private StFltd data_array;
		private StNIFL mStNIFL;

		public FLTD()
		{

		}
		public bool LoadFile(String path)
		{
			//try
			{
				using (var fp = new BinaryIOHelper(new FileStream(path, FileMode.Open, FileAccess.Read), false))
				{
					mStNIFL = new StNIFL(fp);
					fp.SetSkip(mStNIFL.mNIFL.rel0_offset);
					fp.SkipSeek((int)mStNIFL.mREL0.entry);
					data_array = new StFltd(fp);
				}
			}
			/*catch (Exception e)
			{
				return false;
			}*/
			return true;
		}
		public bool SaveFile(String path, bool format/*unused for a while*/)
		{
			try
			{
				using (BinaryIOHelper fp = new BinaryIOHelper(new FileStream(path, FileMode.Create, FileAccess.Write), false))
				{
					fp.SetSkip(mStNIFL.mNIFL.rel0_offset);
					if (data_array.IsNGS() == true)
						SaveNGSFormat(fp);
					else
						SaveClassicFormat(fp);
				}
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
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
		public string[] GetRootNode(int index)
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
					nameList[i] = data0.data3[i].name0;
				}
				return nameList;
			}
			else
			{
				Classic.fltd_data0 data0 = (Classic.fltd_data0)data_array.data0[index];
				string[] nameList = new string[data0.count_addr1];
				for (int i = 0; i < data0.count_addr1; i++)
				{
					nameList[i] = data0.data3[i].name;
				}
				return nameList;
			}
		}
		public string[] GetConstrateBones(int a, int b)
		{
			string[] f = null;
			if (data_array.IsNGS() == true)
			{
				NGS.fltd_data3 data3 = ((NGS.fltd_data0)data_array.data0[a]).data3[b];
				switch (data3.format)
				{
					case 7:
						f = new string[2];
						f[0] = data3.name0;
						f[1] = data3.name1;
						break;
					default:
						f = new string[1];
						f[0] = data3.name0;
						break;
				}
			}
			return f;
		}
		public byte GetConstrateFormat(int a, int b) => ((NGS.fltd_data0)data_array.data0[a]).data3[b].format;
		public float[] GetConstrateParam(int a, int b, int c = 0)
		{
			float[] f = null;
			if (data_array.IsNGS() == true)
			{
				NGS.fltd_data3 data3 = ((NGS.fltd_data0)data_array.data0[a]).data3[b];
				switch (data3.format)
				{
					case 0:
						f = new float[0x3];
						f[0] = data3.value0[0];
						f[1] = data3.value0[1];
						f[2] = data3.value0[3];
						break;
					case 1:
						f = new float[0x3];
						f[0] = data3.value0[0];
						f[1] = data3.value0[1];
						f[2] = data3.value0[3];
						break;
					case 3:
						f = new float[0x4];
						f[0] = data3.value0[0];
						f[1] = data3.value0[1];
						f[2] = data3.value0[4];
						f[3] = data3.value0[6];
						break;
					case 5:
						f = new float[0x3];
						f[0] = data3.value0[1];
						f[1] = data3.value0[2];
						f[2] = data3.value0[4];
						break;
					case 7:
						f = new float[0x4];
						switch (c)
						{
							case 0:
								f[0] = data3.value0[1];
								f[1] = data3.value0[5];
								f[2] = data3.value0[6];
								f[3] = data3.value0[7];
								break;
							case 1:
								f[0] = data3.value0[2];
								f[1] = data3.value0[8];
								f[2] = data3.value0[9];
								f[3] = data3.value0[10];
								break;
						}

						break;
					default:
						break;
				}

			}
			return f;
		}
		public void SetConstrateFormat(int a, int b, byte format) {
			((NGS.fltd_data0)data_array.data0[a]).data3[b].format = format; 
		}
		public void SetConstrateParam(int a, int b, float[] f, int c = 0)
		{
			if (this.IsNGS() == true)
			{
				NGS.fltd_data3 data3 = ((NGS.fltd_data0)data_array.data0[a]).data3[b];
				switch (data3.format)
				{
					case 0:
						data3.value0[0] = f[0];
						data3.value0[1] = f[1];
						data3.value0[3] = f[2];
						break;
					case 1:
						data3.value0[0] = f[0];
						data3.value0[1] = f[1];
						data3.value0[3] = f[2];
						break;
					case 3:
						data3.value0[0] = f[0];
						data3.value0[1] = f[1];
						data3.value0[4] = f[2];
						data3.value0[6] = f[3];
						break;
					case 5:
						data3.value0[1] = f[0];
						data3.value0[2] = f[1];
						data3.value0[4] = f[2];
						break;
					case 7:
						if (f.Length == 4)
						{
							switch (c)
							{
								case 0:
									data3.value0[1] = f[0];
									data3.value0[5] = f[1];
									data3.value0[6] = f[2];
									data3.value0[7] = f[3];
									break;
								case 1:
									data3.value0[2] = f[0];
									data3.value0[8] = f[1];
									data3.value0[9] = f[2];
									data3.value0[10] = f[3];
									break;
							}
						}
						break;
					default:
						break;
				}
			}
		}
		public bool IsNGS()
		{
			return data_array.IsNGS();
		}
	}
}