using System.IO;

namespace FLTD_lib
{
    public partial class FLTD
    {
        private void SaveNGSFormat(BinaryIOHelper fp) {
            // NIFL
            fp.WriteUInt32(0x4E49464C);
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
            fp.Seek((int)REL0_OFFSET,SeekOrigin.Begin);
            fp.WriteUInt32((uint)temp);
            fp.Seek((int)temp, SeekOrigin.Begin);
            fp.WriteUInt32(0x4E49464C);
            long REL0_CONTENTS_SIZE = fp.Tell();
            fp.WriteUInt32(0);
            long FLTD_OFFSET = fp.Tell();
            fp.WriteUInt32(0);
            fp.WriteUInt32(0x0);

            // FLTD
            fp.WriteUInt32(0xffffffff);

            foreach( NGS.fltd_data0 data0 in data_array.data0)
            {
                for(int i = 0; i < data0.count_addr0 + data0.count_addr1; i++)
                {
                    fp.WriteUInt32(0);
                }

            }
        }
    }
}
