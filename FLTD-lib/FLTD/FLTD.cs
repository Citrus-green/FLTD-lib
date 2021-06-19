using System;
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
		public void LoadFile(String path)
		{
			using (var fp = new BinaryIOHelper(new FileStream(path, FileMode.Open, FileAccess.Read), false))
			{
				mStNIFL = new StNIFL(fp);
				fp.SetSkip(mStNIFL.mNIFL.rel0_offset);
				fp.SkipSeek((int)mStNIFL.mREL0.entry);
				data_array = new StFltd(fp);
			}
		}
		public void SaveFile(String path,bool format/*unused for a while*/)
		{
			using (BinaryIOHelper fp = new BinaryIOHelper(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write), false))
			{
				fp.SetSkip(mStNIFL.mNIFL.rel0_offset);
				if (data_array.IsNGS() == true)
					SaveNGSFormat(fp);
				else
					SaveClassicFormat(fp);
			}
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
