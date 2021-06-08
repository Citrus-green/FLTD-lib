using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FLTD_lib
{
	public class FltdNGS
	{

		private stfltd data_array;
		private StNIFL mStNIFL;

		public FltdNGS()
		{

		}
		public void loadFile(String path)
		{
			using (var fp = new BinaryIOHelper(new FileStream(path, FileMode.Open, FileAccess.Read), false))
			{
				mStNIFL = new StNIFL(fp);
				fp.SetSkip(mStNIFL.mNIFL.rel0_offset);
				fp.SkipSeek((int)mStNIFL.mREL0.entry);
				data_array = new stfltd(fp);
			}
		}
		private void readStFltd(BinaryReader fp)
		{

		}
		public string[] GetAssignList()
		{
			string[] str = new string[data_array.count_addr0];
			for (int i = 0; i < data_array.count_addr0; i++)
				str[i] = data_array.data0[i].nameList[0];
			return str;
		}
		public string[] GetRootMode(int index)
		{
			return data_array.data0[index].nameList;
		}
		public string[] GetConstraintList(int index)
		{
			string[] nameList = new string[data_array.data0[index].count_addr1];
			for (int i = 0; i < data_array.data0[index].count_addr1; i++)
			{
				nameList[i] = data_array.data0[index].data3[i].name;
			}
			return nameList;
		}

	}
}
